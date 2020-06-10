using System;
using System.Windows.Forms;

namespace Esoft_Project
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            ShowClient();
        }

        private void labelFirstName_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //создаём новый экземпляр класса Клиент
            ClientsSet clientSet = new ClientsSet();
            //делаем ссылку на объект, который хранится в textBox-ах
            clientSet.FirstName = textBoxFirstName.Text;
            clientSet.MiddleName = textBoxMiddleName.Text;
            clientSet.LastName = textBoxLastName.Text;
            clientSet.Phone = textBoxPhone.Text;
            clientSet.Email = textBoxEmail.Text;
            //добавляем в таблицу ClientsSet нового clientSet
            Program.esoft_Project.ClientsSet.Add(clientSet);
            //сохраняем изменения в модели esoft_ProjectClients
            Program.esoft_Project.SaveChanges();
            ShowClient();
        }
        void ShowClient()
        {
            //предварительно очищаем listView
            listViewClient.Items.Clear();
            //проходимся по коллекции клиентов, которые находятся в базе с помощью foreach
            foreach(ClientsSet clientsSet in Program.esoft_Project.ClientsSet)
            {
                //создаем новый элемент в listView
                //для этого создаем массив строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    //указываем необходимые поля
                    clientsSet.id.ToString(), clientsSet.FirstName, clientsSet.MiddleName,
                    clientsSet.LastName, clientsSet.Phone, clientsSet.Email
                });
                //указываем по какому тегу будем брать элементы
                item.Tag = clientsSet;
                //добавляем элементы в listView для отображения
                listViewClient.Items.Add(item);
            }
            //выравниваем колонки в listView
            listViewClient.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //условие, если в listView выбран 1 элемент
            if (listViewClient.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу
                ClientsSet clientsSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
                //указываем что может быть изменено
                clientsSet.FirstName = textBoxFirstName.Text;
                clientsSet.MiddleName = textBoxMiddleName.Text;
                clientsSet.LastName = textBoxLastName.Text;
                clientsSet.Phone = textBoxPhone.Text;
                clientsSet.Email = textBoxEmail.Text;
                //сохраняем изменения в модели
                Program.esoft_Project.SaveChanges();
                //отображение в listView
                ShowClient();
            }
        }

        private void FormClient_Load(object sender, EventArgs e)
        {

        }
        private void listViewClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            //условие, если выбран 1 элемент
            if (listViewClient.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу
                ClientsSet clientSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
                //указываем, что может быть изменено
                textBoxFirstName.Text = clientSet.FirstName;
                textBoxMiddleName.Text = clientSet.MiddleName;
                textBoxLastName.Text = clientSet.LastName;
                textBoxPhone.Text = clientSet.Phone;
                textBoxEmail.Text = clientSet.Email;

                ShowSupplySet(clientSet);
                ShowDemandSet(clientSet);
            }
            else
            {
                //условие, инача, если не выбран ни один элемент, то задаем пустые поля
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";

                listViewSupplySet.Items.Clear();
                listViewDemandSet.Items.Clear();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            //пробуем совершить действие
            try
            {
                if (listViewClient.SelectedItems.Count == 1)
                {
                    //ищем этот элемент
                    ClientsSet clientsSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
                    //удаляем из модели и базы данных
                    Program.esoft_Project.ClientsSet.Remove(clientsSet);
                    //сохраняем изменения
                    Program.esoft_Project.SaveChanges();
                    //отображаем обновленный список
                    ShowClient();
                }
                //очищаем textBox-ы
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
            }
            //если возникает какая-то ошибка, к примеру, запись используется, выводим всплывающее сообщение
            catch 
            {
                //вызываем метод для всплывающего окна, в котором указываем текст, заголовок, кнопку и иконку
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ShowSupplySet(ClientsSet clientsSet)
        {
            //очищаем listView
            listViewSupplySet.Items.Clear();
            //проходим по коллекции
            foreach (SupplySet supply in Program.esoft_Project.SupplySet)
            {
                if (clientsSet.id == supply.IdClient)
                {
                    //создаем новый элемент в listView с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                    supply.AgentSet.LastName+" "+supply.AgentSet.FirstName.Remove(1)+"."+supply.AgentSet.MiddleName.Remove(1)+".",
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
        void ShowDemandSet(ClientsSet clientsSet)
        {
            listViewDemandSet.Items.Clear();
            foreach (DemandSet demand in Program.esoft_Project.DemandSet)
            {
                if (clientsSet.id == demand.IdClient)
                {
                    string type;
                    if (demand.Type == 0)
                        type = "Квартира";
                    else if (demand.Type == 1)
                        type = "Дом";
                    else type = "Земля";
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demand.AgentSet.LastName+" "+demand.AgentSet.FirstName.Remove(1)+"."+demand.AgentSet.MiddleName.Remove(1)+".",
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
