using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Threading;

namespace BasicRxSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _repo = new ObservableHelper<Customer>();
            //SearchList("");//load all
            
            ControlScheduler cs = new ControlScheduler(this);//WE NEED THIS.
            Observable.FromEventPattern(h => textBox1.TextChanged += h,
                                        h => textBox1.TextChanged -= h)//tell Rx about our event
                .Throttle(TimeSpan.FromMilliseconds(500), cs)///throttle
                .Do(a => SearchList(textBox1.Text))//do this method 
                .Subscribe();//this is where we tell it to begin all the magic
        }
        IObservableHelper<Customer, Database1Entities> _repo;
        void SearchList(string query)
        {
            listBox1.Items.Clear();
            listBox1.BeginUpdate();
            var getfn = _repo.GetAllAsObservables
                (d => d.Customers.Where(c => c.FirstName.Contains(query) || c.LastName.Contains(query)));
            getfn.ObserveOn(this).Subscribe(custList =>
                {
                    foreach (var item in custList)
                    {
                        listBox1.Items.Add(string.Format("{0} {1},{2}", item.FirstName, item.LastName, item.Age));
                    }
                    listBox1.EndUpdate();
                });
            //we could also do ObserveOn(WindowsFormsSynchronizationContext.Current)
        }
    }
}
