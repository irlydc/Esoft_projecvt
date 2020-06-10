using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project.Forms
{
    public partial class FormAgent : Form
    {
        public FormAgent()
        {
            InitializeComponent();
            ShowAgent();
        }

        private void FormAgent_Load(object sender, EventArgs e)
        {

        }
        private void textBoxDealShare_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AgentSet agentSet = new AgentSet();
                agentSet.FirstName = textBoxFirstName.Text;
                agentSet.MiddleName = textBoxMiddleName.Text;
                agentSet.LastName = textBoxLastName.Text;
                if (textBoxDealShare.Text != "") 
                    agentSet.DealShare = Convert.ToInt32(textBoxDealShare.Text);
                if (agentSet.FirstName == ""||agentSet.MiddleName == "" || agentSet.LastName == "")
                {
                    throw new Exception("Не заполнены поля ФИО");
                }
                if (agentSet.DealShare <0 || agentSet.DealShare > 100)
                {
                    throw new Exception("Доля от комиссии должна находится в диапозоне от 0 до 100");
                }
                Program.esoft_Project.AgentSet.Add(agentSet);
                Program.esoft_Project.SaveChanges();
                ShowAgent();
            }
            catch (Exception ex) { MessageBox.Show(""+ex.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }
        void ShowAgent()
        {
            listViewAgent.Items.Clear();
            foreach(AgentSet agentSet in Program.esoft_Project.AgentSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    agentSet.Id.ToString(), agentSet.FirstName, agentSet.MiddleName,agentSet.LastName,
                    agentSet.DealShare.ToString()
                });
                item.Tag = agentSet;
                listViewAgent.Items.Add(item);
            }
            listViewAgent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try 
            {
                if (listViewAgent.SelectedItems.Count == 1)
                {
                    AgentSet agentSet = listViewAgent.SelectedItems[0].Tag as AgentSet;
                    agentSet.FirstName = textBoxFirstName.Text;
                    agentSet.MiddleName = textBoxMiddleName.Text;
                    agentSet.LastName = textBoxLastName.Text;
                    if (textBoxDealShare.Text != "")
                        agentSet.DealShare = Convert.ToInt32(textBoxDealShare.Text);
                    if (textBoxDealShare.Text == "")
                        agentSet.DealShare = null;
                    if (agentSet.FirstName == "" || agentSet.MiddleName == "" || agentSet.LastName == "")
                    {
                        throw new Exception("Не заполнены поля ФИО");
                    }
                    if (agentSet.DealShare < 0 || agentSet.DealShare > 100)
                    {
                        throw new Exception("Доля от комиссии должна находится в диапозоне от 0 до 100");
                    }
                    Program.esoft_Project.SaveChanges();
                    ShowAgent();
                }
            }
            catch (Exception ex) { MessageBox.Show("" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void listViewAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewAgent.SelectedItems.Count == 1)
            {
                AgentSet agentSet = listViewAgent.SelectedItems[0].Tag as AgentSet;
                textBoxFirstName.Text = agentSet.FirstName;
                textBoxMiddleName.Text = agentSet.MiddleName;
                textBoxLastName.Text = agentSet.LastName;
                textBoxDealShare.Text = agentSet.DealShare.ToString();

                ShowSupplySet(agentSet);
                ShowDemandSet(agentSet);
            }
            else
            {
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealShare.Text = "";

                listViewSupplySet.Items.Clear();
                listViewDemandSet.Items.Clear();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewAgent.SelectedItems.Count == 1)
                {
                    AgentSet agentSet = listViewAgent.SelectedItems[0].Tag as AgentSet;
                    Program.esoft_Project.AgentSet.Remove(agentSet);
                    Program.esoft_Project.SaveChanges();
                    ShowAgent();
                }
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealShare.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ShowSupplySet(AgentSet agentSet)
        {
            //очищаем listView
            listViewSupplySet.Items.Clear();
            //проходим по коллекции
            foreach (SupplySet supply in Program.esoft_Project.SupplySet)
            {
                if (agentSet.Id == supply.IdAgent)
                {
                    //создаем новый элемент в listView с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                    supply.ClientsSet.LastName+" "+supply.ClientsSet.FirstName.Remove(1)+"."+supply.ClientsSet.MiddleName.Remove(1)+".",
                    //адрес ОН
                    "г. "+supply.RealEstateSet.Address_City+", ул. "+supply.RealEstateSet.Address_Street+", д. "+
                    supply.RealEstateSet.Address_House+", кв. "+supply.RealEstateSet.Address_Number,
                    //цена
                    supply.Price.ToString()
                    });
                    //указываем по какому тегу выбраны элементы
                    item.Tag = supply;
                    //добавляем элементы в listView
                    listViewSupplySet.Items.Add(item);
                }
            }
        }
        void ShowDemandSet(AgentSet agentSet)
        {
            listViewDemandSet.Items.Clear();
            foreach (DemandSet demand in Program.esoft_Project.DemandSet)
            {
                if (agentSet.Id == demand.IdAgent)
                {
                    string type;
                    if (demand.Type == 0)
                        type = "Квартира";
                    else if (demand.Type == 1)
                        type = "Дом";
                    else type = "Земля";
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demand.ClientsSet.LastName+" "+demand.ClientsSet.FirstName.Remove(1)+"."+demand.ClientsSet.MiddleName.Remove(1)+".",
                        demand.MinPrice.ToString(),
                        demand.MaxPrice.ToString(),
                        demand.MinArea.ToString(),
                        demand.MaxArea.ToString(),
                        type
                    });
                    item.Tag = demand;
                    listViewDemandSet.Items.Add(item);
                }
            }
        }

        private void labelDemand_Click(object sender, EventArgs e)
        {

        }
    }
}
