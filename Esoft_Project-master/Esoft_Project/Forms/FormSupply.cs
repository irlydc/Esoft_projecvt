using System;
using System.Windows.Forms;

namespace Esoft_Project.Forms
{
    public partial class FormSupply : Form
    {
        public FormSupply()
        {
            InitializeComponent();
            ShowAgents();
            ShowClients();
            ShowRealEstate();
            ShowSupplySet();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //проверяем, что все поля были заполнены
            if (comboBoxAgents.SelectedItem != null && comboBoxClients.SelectedItem != null && comboBoxRealEstate.SelectedItem != null && textBoxPrice.Text != "")
            {
                //создаем новый экземпляр класса Предложение
                SupplySet supply = new SupplySet();
                //из выьранной строки в comboBoxAgents отделяем Id Риелтора (он отделен точкой) и делаем ссылку supply.IdAgent
                supply.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                //точно также с клиентом и объектом недвижимости
                supply.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                supply.IdRealEstate = Convert.ToInt32(comboBoxRealEstate.SelectedItem.ToString().Split('.')[0]);
                //цена чаще всего превосходит миллион, поэтому используем Int64
                supply.Price = Convert.ToInt64(textBoxPrice.Text);
                //добавляем и сохраняем в таблицу SupplySet
                Program.esoft_Project.SupplySet.Add(supply);
                Program.esoft_Project.SaveChanges();
                ShowSupplySet();
            }
            else MessageBox.Show("Данные не выбраны!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewSupplySet.SelectedItems.Count == 1)
                {
                    SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                    supply.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                    supply.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                    supply.IdRealEstate = Convert.ToInt32(comboBoxRealEstate.SelectedItem.ToString().Split('.')[0]);
                    if (textBoxPrice.Text != "")
                        supply.Price = Convert.ToInt64(textBoxPrice.Text);
                    else throw new Exception("Поле цены не заполнено");
                        Program.esoft_Project.SaveChanges();
                    ShowSupplySet();
                }
            }
            catch (Exception ex) { MessageBox.Show("" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewSupplySet.SelectedItems.Count == 1)
                {
                    SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                    Program.esoft_Project.SupplySet.Remove(supply);
                    Program.esoft_Project.SaveChanges();
                    ShowSupplySet();
                }
                comboBoxAgents.Text = null;
                comboBoxClients.Text = null;
                comboBoxRealEstate.Text = null;
                textBoxPrice.Text = "";
            }
            catch { MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        void ShowAgents()
        {
            //очищаем comboBox
            comboBoxAgents.Items.Clear();
            foreach(AgentSet agentSet in Program.esoft_Project.AgentSet)
            {
                //добавляем информацию, которую хотим видеть в строке comboBox-а
                //можно настроить по своему усмотрению, добавить/удалить некоторые элементы и сокращения
                //главное, не убирать Id, так как он нужен для дальнейшей работы
                string[] item = { agentSet.Id.ToString() + ".", agentSet.FirstName, agentSet.MiddleName, agentSet.LastName };
                comboBoxAgents.Items.Add(string.Join( " ", item));
            }
        }
        void ShowRealEstate()
        {
            //очищаем comboBox
            comboBoxRealEstate.Items.Clear();
            foreach (RealEstateSet realEstateSet in Program.esoft_Project.RealEstateSet)
            {
                //добавляем информацию, которую хотим видеть в строке comboBox-а
                //можно настроить по своему усмотрению, добавить/удалить некоторые элементы и сокращения
                //главное, не убирать Id, так как он нужен для дальнейшей работы
                string[] item = {realEstateSet.Id.ToString() + ".", realEstateSet.Address_City + ",",realEstateSet.Address_Street + ",",
                "д. "+realEstateSet.Address_House + ",","кв. "+realEstateSet.Address_Number  };
                comboBoxRealEstate.Items.Add(string.Join(" ", item));
            }
        }
        void ShowClients()
        {
            //очищаем comboBox
            comboBoxClients.Items.Clear();
            foreach (ClientsSet clientSet in Program.esoft_Project.ClientsSet)
            {
                //добавляем информацию, которую хотим видеть в строке comboBox-а
                //можно настроить по своему усмотрению, добавить/удалить некоторые элементы и сокращения
                //главное, не убирать Id, так как он нужен для дальнейшей работы
                string[] item = { clientSet.id.ToString() + ".", clientSet.FirstName, clientSet.MiddleName, clientSet.LastName };
                comboBoxClients.Items.Add(string.Join(" ", item));
            }
        }
        void ShowSupplySet()
        {
            //очищаем listView
            listViewSupplySet.Items.Clear();
            //проходим по коллекции
            foreach (SupplySet supply in Program.esoft_Project.SupplySet)
            {
                //создаем новый элемент в listView с помощью массива строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    //Id риелтора
                    supply.IdAgent.ToString(),
                    //Фио риелтора
                    supply.AgentSet.LastName+" "+supply.AgentSet.FirstName+" "+supply.AgentSet.MiddleName,
                    //Id клиента
                    supply.IdClient.ToString(),
                    //ФИО клиента
                    supply.ClientsSet.LastName+" "+supply.ClientsSet.FirstName+" "+supply.ClientsSet.MiddleName,
                    //Id ОН
                    supply.IdRealEstate.ToString(),
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

        private void listViewSupplySet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSupplySet.SelectedItems.Count == 1)
            {
                SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(supply.IdAgent.ToString());
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(supply.IdClient.ToString());
                comboBoxRealEstate.SelectedIndex = comboBoxRealEstate.FindString(supply.IdRealEstate.ToString());
                textBoxPrice.Text = supply.Price.ToString();
            }
            else
            {
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                comboBoxRealEstate.SelectedItem = null;
                textBoxPrice.Text = "";
            }
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }

        private void comboBoxAgents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
