using System;
using System.Windows.Forms;

namespace Esoft_Project.Forms
{
    public partial class FormRealEstate : Form
    {
        public FormRealEstate()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            ShowRealEstateSet();
        }

        private void FormRealEstate_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //изменения формы, если выбрана строчка "Квартира" (индекс = 0)
            if (comboBoxType.SelectedIndex == 0)
            {
                listViewRealEstateSet_Apartment.Visible = true;
                labelFloor.Visible = true;
                textBoxFloor.Visible = true;
                labelRooms.Visible = true;
                textBoxRooms.Visible = true;

                //скрываем ненужные элементы
                listViewRealEstateSet_House.Visible = false;
                listViewRealEstateSet_Land.Visible = false;
                labelTotalFloors.Visible = false;
                textBoxTotalFloors.Visible = false;

                //очищаем все видимые элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxRooms.Text = "";
                textBoxFloor.Text = "";
            }
            //изменения формы, елси выбрана строчка "Дом" (индекс = 1)
            else if (comboBoxType.SelectedIndex == 1)
            {
                //делаем видимыми нужные элементы
                listViewRealEstateSet_House.Visible = true;
                labelTotalFloors.Visible = true;
                textBoxTotalFloors.Visible = true;

                //скрываем ненужные жлементы
                listViewRealEstateSet_Apartment.Visible = false;
                listViewRealEstateSet_Land.Visible = false;
                labelFloor.Visible = false;
                textBoxFloor.Visible = false;
                labelRooms.Visible = false;
                textBoxRooms.Visible = false;

                //очищаем  видимые элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxTotalFloors.Text = "";
            }
            else if (comboBoxType.SelectedIndex == 2)
            {
                listViewRealEstateSet_Land.Visible = true;

                listViewRealEstateSet_House.Visible = false;
                listViewRealEstateSet_Apartment.Visible = false;
                labelFloor.Visible = false;
                textBoxFloor.Visible = false;
                labelRooms.Visible = false;
                textBoxRooms.Visible = false;
                labelTotalFloors.Visible = false;
                textBoxTotalFloors.Visible = false;

                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
            }
        }
        void ShowRealEstateSet()
        {
            //очищаем все listView
            listViewRealEstateSet_Apartment.Items.Clear();
            listViewRealEstateSet_House.Items.Clear();
            listViewRealEstateSet_Land.Items.Clear();
            //проходим по коллекции клиентов, которые находятся в базе с помощью foreach
            foreach (RealEstateSet realEstate in Program.esoft_Project.RealEstateSet)
            {
                //отображение квартир в listViewRealEstateSet_Apartment
                if (realEstate.Type == 0)
                {
                    //создадим новый элемент в listViewRealEstateSet_Apartment с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        //указываем необходимые поля
                        realEstate.Address_City, realEstate.Address_Street,realEstate.Address_House,
                        realEstate.Address_Number, realEstate.Coordinate_latitude.ToString(), realEstate.Coordinate_longitude.ToString(),
                        realEstate.TotalArea.ToString(),realEstate.Rooms.ToString(), realEstate.Floor.ToString()
                    });
                    //указываем по какому тегу выбираем элементы
                    item.Tag = realEstate;
                    //добавляем элементы в listViewRealEstateSet_Apartment для отображения 
                    listViewRealEstateSet_Apartment.Items.Add(item);
                }
                //отображение домов в listViewRealEstateSet_House
                else if (realEstate.Type == 1)
                {
                    //создадим новый элемент в listViewRealEstateSet_House с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        //указываем необходимые поля
                        realEstate.Address_City, realEstate.Address_Street,realEstate.Address_House,
                        realEstate.Address_Number, realEstate.Coordinate_latitude.ToString(), realEstate.Coordinate_longitude.ToString(),
                        realEstate.TotalArea.ToString(),realEstate.TotalFloors.ToString()
                    });
                    //указываем по какому тегу выбираем элементы
                    item.Tag = realEstate;
                    //добавляем элементы в listViewRealEstateSet_House для отображения 
                    listViewRealEstateSet_House.Items.Add(item);
                }
                else
                {
                    //создадим новый элемент в listViewRealEstateSet_Land с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        //указываем необходимые поля
                        realEstate.Address_City, realEstate.Address_Street,realEstate.Address_House,
                        realEstate.Address_Number, realEstate.Coordinate_latitude.ToString(), realEstate.Coordinate_longitude.ToString(),
                        realEstate.TotalArea.ToString()
                    });
                    //указываем по какому тегу выбираем элементы
                    item.Tag = realEstate;
                    //добавляем элементы в listViewRealEstateSet_Land для отображения 
                    listViewRealEstateSet_Land.Items.Add(item);
                }
            }
            //выравниваем столбцы во всех listView
            listViewRealEstateSet_Apartment.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_House.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_Land.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
                //создаем новый экземпляр класса "Объект недвижимости"
                RealEstateSet realEstate = new RealEstateSet();
                //делаем ссылку на объект, который хранится в textBox-ах
                realEstate.Address_City = textBoxAddress_City.Text;
                realEstate.Address_House = textBoxAddress_House.Text;
                realEstate.Address_Street = textBoxAddress_Street.Text;
                realEstate.Address_Number = textBoxAddress_Number.Text;
                if (textBoxCoordinate_latitude.Text != "") { realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text); }
                if (realEstate.Coordinate_latitude < -90)
                {
                    realEstate.Coordinate_latitude = -90;
                    textBoxCoordinate_latitude.Text = "-90";
                }
                if (realEstate.Coordinate_latitude > 90)
                {
                    realEstate.Coordinate_latitude = 90;
                    textBoxCoordinate_latitude.Text = "90";
                }
                if (textBoxCoordinate_longitude.Text != "") { realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text); }
                if (realEstate.Coordinate_longitude < -180)
                {
                realEstate.Coordinate_longitude = -180;
                textBoxCoordinate_longitude.Text = "-180";
                }
                if (realEstate.Coordinate_longitude > 180)
                {
                    realEstate.Coordinate_longitude = 180;
                    textBoxCoordinate_longitude.Text = "180";
                }
                if (textBoxTotalArea.Text != "") { realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text); }    
                //дополнительные поля для типа "Квартира"
                if (comboBoxType.SelectedIndex == 0)
                {
                    realEstate.Type = 0;
                    if (textBoxRooms.Text != "") { realEstate.Rooms = Convert.ToInt32(textBoxRooms.Text); };
                    if (textBoxFloor.Text != "") { realEstate.Floor = Convert.ToInt32(textBoxFloor.Text); }
                }
                //дополнительные поля для типа "Дом"
                else if (comboBoxType.SelectedIndex == 1)
                {
                    realEstate.Type = 1;
                    if (textBoxTotalFloors.Text != "") { realEstate.TotalFloors = Convert.ToInt32(textBoxTotalFloors.Text); }
                }
                //дополнительные поля для типа "Земля"
                else
                {
                    realEstate.Type = 2;
                }
                
                //добавляем в таблицу RealEstateSet новый объект недвижимости realEstate
                Program.esoft_Project.RealEstateSet.Add(realEstate);
                //сохраняем изменения
                Program.esoft_Project.SaveChanges();
                ShowRealEstateSet();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //выбран тип "Квартира", работа с listViewRealEstateSet_Apartment
            if (comboBoxType.SelectedIndex == 0)
            {
                //если в listView выбран элемент
                if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
                {
                    //Ищем элементы из таблицы по тегу
                    RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                    //указываем что может быть изменено
                    realEstate.Address_City = textBoxAddress_City.Text;
                    realEstate.Address_House = textBoxAddress_House.Text;
                    realEstate.Address_Street = textBoxAddress_Street.Text;
                    realEstate.Address_Number = textBoxAddress_Number.Text;
                    if (textBoxCoordinate_latitude.Text != "")
                        realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
                    else realEstate.Coordinate_latitude = null;
                    if (realEstate.Coordinate_latitude < -90)
                    {
                        realEstate.Coordinate_latitude = -90;
                        textBoxCoordinate_latitude.Text = "-90";
                    }
                    if (realEstate.Coordinate_latitude > 90)
                    {
                        realEstate.Coordinate_latitude = 90;
                        textBoxCoordinate_latitude.Text = "90";
                    }
                    if (textBoxCoordinate_longitude.Text != "")
                        realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
                    else realEstate.Coordinate_longitude = null;
                    if (realEstate.Coordinate_longitude < -180)
                    {
                        realEstate.Coordinate_longitude = -180;
                        textBoxCoordinate_longitude.Text = "-180";
                    }
                    if (realEstate.Coordinate_longitude > 180)
                    {
                        realEstate.Coordinate_longitude = 180;
                        textBoxCoordinate_longitude.Text = "180";
                    }
                    if (textBoxTotalArea.Text != "")
                        realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
                    else realEstate.TotalArea = null;
                    if (textBoxRooms.Text != "")
                        realEstate.Rooms = Convert.ToInt32(textBoxRooms.Text);
                    else realEstate.Rooms = null;
                    if (textBoxFloor.Text != "")
                        realEstate.Floor = Convert.ToInt32(textBoxFloor.Text);
                    else realEstate.Floor = null;
                    //save
                    Program.esoft_Project.SaveChanges();
                    //отображаем в listViewRealEstateSet_Apartment
                    ShowRealEstateSet();
                }
            }
            //выбран тип "Дом"
            else if (comboBoxType.SelectedIndex == 1)
            {
                //если в listView выбран элемент
                if (listViewRealEstateSet_House.SelectedItems.Count == 1)
                {
                    //ищем по тегу
                    RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                    //указываем, что может быть изменено
                    realEstate.Address_City = textBoxAddress_City.Text;
                    realEstate.Address_House = textBoxAddress_House.Text;
                    realEstate.Address_Street = textBoxAddress_Street.Text;
                    realEstate.Address_Number = textBoxAddress_Number.Text;
                    if (textBoxCoordinate_latitude.Text != "")
                        realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
                    else realEstate.Coordinate_latitude = null;
                    if (realEstate.Coordinate_latitude < -90)
                    {
                        realEstate.Coordinate_latitude = -90;
                        textBoxCoordinate_latitude.Text = "-90";
                    }
                    if (realEstate.Coordinate_latitude > 90)
                    {
                        realEstate.Coordinate_latitude = 90;
                        textBoxCoordinate_latitude.Text = "90";
                    }
                    if (textBoxCoordinate_longitude.Text != "")
                        realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
                    else realEstate.Coordinate_longitude = null;
                    if (realEstate.Coordinate_longitude < -180)
                    {
                        realEstate.Coordinate_longitude = -180;
                        textBoxCoordinate_longitude.Text = "-180";
                    }
                    if (realEstate.Coordinate_longitude > 180)
                    {
                        realEstate.Coordinate_longitude = 180;
                        textBoxCoordinate_longitude.Text = "180";
                    }
                    if (textBoxTotalArea.Text != "")
                        realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
                    else realEstate.TotalArea = null;
                    if (textBoxTotalFloors.Text != "")
                        realEstate.TotalFloors = Convert.ToInt32(textBoxTotalFloors.Text);
                    else realEstate.TotalFloors = null;
                    //save
                    Program.esoft_Project.SaveChanges();
                    ShowRealEstateSet();
                }
            }
            else
            {
               
                if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
                {
                    RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                    realEstate.Address_City = textBoxAddress_City.Text;
                    realEstate.Address_House = textBoxAddress_House.Text;
                    realEstate.Address_Street = textBoxAddress_Street.Text;
                    realEstate.Address_Number = textBoxAddress_Number.Text;
                    if (textBoxCoordinate_latitude.Text != "")
                        realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
                    else realEstate.Coordinate_latitude = null;
                    if (realEstate.Coordinate_latitude < -90)
                    {
                        realEstate.Coordinate_latitude = -90;
                        textBoxCoordinate_latitude.Text = "-90";
                    }
                    if (realEstate.Coordinate_latitude > 90)
                    {
                        realEstate.Coordinate_latitude = 90;
                        textBoxCoordinate_latitude.Text = "90";
                    }
                    if (textBoxCoordinate_longitude.Text != "")
                        realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
                    else realEstate.Coordinate_longitude = null;
                    if (realEstate.Coordinate_longitude < -180)
                    {
                        realEstate.Coordinate_longitude = -180;
                        textBoxCoordinate_longitude.Text = "-180";
                    }
                    if (realEstate.Coordinate_longitude > 180)
                    {
                        realEstate.Coordinate_longitude = 180;
                        textBoxCoordinate_longitude.Text = "180";
                    }
                    if (textBoxTotalArea.Text != "")
                        realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
                    else realEstate.TotalArea = null;
                    Program.esoft_Project.SaveChanges();
                    ShowRealEstateSet();
                }
                
            }
            
        }

        private void listViewRealEstateSet_Apartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //если выбран 1 элемент
            if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
            {
                RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
                textBoxRooms.Text = realEstate.Rooms.ToString();
                textBoxFloor.Text = realEstate.Floor.ToString();
            }
            else // если не выбран ни один элемент
            {
                textBoxAddress_City.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxRooms.Text = "";
                textBoxFloor.Text = "";
            }
        }

        private void listViewRealEstateSet_Land_SelectedIndexChanged(object sender, EventArgs e)
        {
            //если выбран 1 элемент
            if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
            {
                RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
            }
            else // если не выбран ни один элемент
            {
                textBoxAddress_City.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
            }
        }

        private void listViewRealEstateSet_House_SelectedIndexChanged(object sender, EventArgs e)
        {
            //если выбран 1 элемент
            if (listViewRealEstateSet_House.SelectedItems.Count == 1)
            {
                RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
                textBoxTotalFloors.Text = realEstate.Floor.ToString();
            }
            else // если не выбран ни один элемент
            {
                textBoxAddress_City.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxTotalFloors.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
                    {
                        RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                        Program.esoft_Project.RealEstateSet.Remove(realEstate);
                        Program.esoft_Project.SaveChanges();
                        ShowRealEstateSet();
                    }
                    textBoxAddress_City.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                    textBoxRooms.Text = "";
                    textBoxFloor.Text = "";
                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    if (listViewRealEstateSet_House.SelectedItems.Count == 1)
                    {
                        RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                        Program.esoft_Project.RealEstateSet.Remove(realEstate);
                        Program.esoft_Project.SaveChanges();
                        ShowRealEstateSet();
                    }
                    textBoxAddress_City.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                    textBoxTotalFloors.Text = "";
                }
                else
                {
                    if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
                    {
                        RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                        Program.esoft_Project.RealEstateSet.Remove(realEstate);
                        Program.esoft_Project.SaveChanges();
                        ShowRealEstateSet();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewRealEstateSet_Land_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FormRealEstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number !=45 && number !=43) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }

        private void textBoxFloor_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
