using System;
using System.Windows.Forms;

namespace Esoft_Project
{
    public partial class FormDemand : Form
    {
        public FormDemand()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            ShowAgents();
            ShowClients();
            ShowDemandSet();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex == 0)
            {
                labelMinRooms.Visible = true;
                labelMaxRooms.Visible = true;
                labelMinFloor.Visible = true;
                labelMaxFloor.Visible = true;
                labelMinFloors.Visible = false;
                labelMaxFloors.Visible = false;

                textBoxMinRooms.Visible = true;
                textBoxMaxRooms.Visible = true;
                textBoxMinFloor.Visible = true;
                textBoxMaxFloor.Visible = true;
                textBoxMinFloors.Visible = false;
                textBoxMaxFloors.Visible = false;
                textBoxMinPrice.Text = "";
                textBoxMaxPrice.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinRooms.Text = "";
                textBoxMaxRooms.Text = "";
                textBoxMinFloor.Text = "";
                textBoxMaxFloor.Text = "";

                listViewDemand_Appartment.Visible = true;
                listViewDemand_House.Visible = false;
                listViewDemand_Land.Visible = false;
                ShowDemandSet();
            }

            else if (comboBoxType.SelectedIndex == 1)
            {
                labelMinRooms.Visible = false;
                labelMaxRooms.Visible = false;
                labelMinFloor.Visible = false;
                labelMaxFloor.Visible = false;
                labelMinFloors.Visible = true;
                labelMaxFloors.Visible = true;

                textBoxMinRooms.Visible = false;
                textBoxMaxRooms.Visible = false;
                textBoxMinFloor.Visible = false;
                textBoxMaxFloor.Visible = false;
                textBoxMinFloors.Visible = true;
                textBoxMaxFloors.Visible = true;
                textBoxMinPrice.Text = "";
                textBoxMaxPrice.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";

                listViewDemand_Appartment.Visible = false;
                listViewDemand_House.Visible = true;
                listViewDemand_Land.Visible = false;
                ShowDemandSet();
            }
            else
            {
                labelMinRooms.Visible = false;
                labelMaxRooms.Visible = false;
                labelMinFloor.Visible = false;
                labelMaxFloor.Visible = false;
                labelMinFloors.Visible = false;
                labelMaxFloors.Visible = false;

                textBoxMinRooms.Visible = false;
                textBoxMaxRooms.Visible = false;
                textBoxMinFloor.Visible = false;
                textBoxMaxFloor.Visible = false;
                textBoxMinFloors.Visible = false;
                textBoxMaxFloors.Visible = false;

                listViewDemand_Appartment.Visible = false;
                listViewDemand_House.Visible = false;
                listViewDemand_Land.Visible = true;
                ShowDemandSet();
            }
        }
        private void FormDemand_Load(object sender, EventArgs e)
        {

        }
        void ShowAgents()
        {
            comboBoxAgents.Items.Clear();
            foreach (AgentSet agentSet in Program.esoft_Project.AgentSet)
            {
                string[] item = { agentSet.Id.ToString() + ".", agentSet.FirstName, agentSet.MiddleName, agentSet.LastName };
                comboBoxAgents.Items.Add(string.Join(" ", item));
            }
        }
        void ShowClients()
        {
            comboBoxClients.Items.Clear();
            foreach (ClientsSet clientSet in Program.esoft_Project.ClientsSet)
            {
                string[] item = { clientSet.id.ToString() + ".", clientSet.FirstName, clientSet.MiddleName, clientSet.LastName };
                comboBoxClients.Items.Add(string.Join(" ", item));
            }
        }
        

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    if (comboBoxAgents.SelectedItem != null && comboBoxClients.SelectedItem != null && comboBoxType != null)
                    {
                        DemandSet demand = new DemandSet();
                        demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                        demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                        demand.Type = 0;
                        if (textBoxMinPrice.Text != "")
                            demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                        if (textBoxMaxPrice.Text != "")
                            demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                        if (demand.MaxPrice < demand.MinPrice) { throw new ApplicationException("цена"); }
                        if (textBoxMinArea.Text != "")
                            demand.MinArea = Convert.ToInt32(textBoxMinArea.Text);
                        if (textBoxMaxArea.Text != "")
                            demand.MaxArea = Convert.ToInt32(textBoxMaxArea.Text);
                        if (demand.MaxArea < demand.MinArea) { throw new ApplicationException("площадь"); }
                        if (textBoxMinFloor.Text != "")
                            demand.MinFloor = Convert.ToInt32(textBoxMinFloor.Text);
                        if (textBoxMaxFloor.Text != "")
                            demand.MaxFloor = Convert.ToInt32(textBoxMaxFloor.Text);
                        if (demand.MaxFloor < demand.MinFloor) { throw new Exception("этаж"); }
                        if (textBoxMinRooms.Text != "")
                            demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                        if (textBoxMaxRooms.Text != "")
                            demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                        if (demand.MaxRooms < demand.MinRooms) { throw new AggregateException("количество комнат"); }
                        Program.esoft_Project.DemandSet.Add(demand);
                        Program.esoft_Project.SaveChanges();
                        ShowDemandSet();
                    }
                    else MessageBox.Show("Данные не выбраны!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    if (comboBoxAgents.SelectedItem != null && comboBoxClients.SelectedItem != null && comboBoxType != null)
                    {
                        DemandSet demand = new DemandSet();
                        demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                        demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                        demand.Type = 1;
                        if (textBoxMinPrice.Text != "")
                            demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                        if (textBoxMaxPrice.Text != "")
                            demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                        if (demand.MaxPrice < demand.MinPrice) { throw new ApplicationException("цена"); }
                        if (textBoxMinArea.Text != "")
                            demand.MinArea = Convert.ToInt32(textBoxMinArea.Text);
                        if (textBoxMaxArea.Text != "")
                            demand.MaxArea = Convert.ToInt32(textBoxMaxArea.Text);
                        if (demand.MaxArea < demand.MinArea) { throw new ApplicationException("площадь"); }
                        if (textBoxMinFloors.Text != "")
                            demand.MinFloors = Convert.ToInt32(textBoxMinFloors.Text);
                        if (textBoxMaxFloors.Text != "")
                            demand.MaxFloors = Convert.ToInt32(textBoxMaxFloors.Text);
                        if (demand.MaxArea < demand.MinArea) { throw new ApplicationException("этажность"); }
                        Program.esoft_Project.DemandSet.Add(demand);
                        Program.esoft_Project.SaveChanges();
                        ShowDemandSet();
                    }
                    else MessageBox.Show("Данные не выбраны!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (comboBoxAgents.SelectedItem != null && comboBoxClients.SelectedItem != null && comboBoxType != null)
                    {
                        DemandSet demand = new DemandSet();
                        demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                        demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                        demand.Type = 2;
                        if (textBoxMinPrice.Text != "")
                            demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                        if (textBoxMaxPrice.Text != "")
                            demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                        if (demand.MaxPrice < demand.MinPrice) { throw new ApplicationException("цена"); }
                        if (textBoxMinArea.Text != "")
                            demand.MinArea = Convert.ToInt32(textBoxMinArea.Text);
                        if (textBoxMaxArea.Text != "")
                            demand.MaxArea = Convert.ToInt32(textBoxMaxArea.Text);
                        if (demand.MaxArea < demand.MinArea) { throw new ApplicationException("площадь"); }
                        Program.esoft_Project.DemandSet.Add(demand);
                        Program.esoft_Project.SaveChanges();
                        ShowDemandSet();
                    }
                    else MessageBox.Show("Данные не выбраны!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ApplicationException ex) { MessageBox.Show("Максимальная " + ex.Message + " не может быть меньше минимальной!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (AggregateException ex) { MessageBox.Show("Максимальное " + ex.Message + " не может быть меньше минимального!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (Exception ex) { MessageBox.Show("Максимальный " + ex.Message + " не может быть меньше минимального!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void listViewDemand_Appartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDemand_Appartment.SelectedItems.Count == 1)
            {
                DemandSet demand = listViewDemand_Appartment.SelectedItems[0].Tag as DemandSet;
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(demand.IdAgent.ToString());
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(demand.IdClient.ToString());
                textBoxMinPrice.Text = demand.MinPrice.ToString();
                textBoxMaxPrice.Text = demand.MaxPrice.ToString();
                textBoxMinArea.Text = demand.MinArea.ToString();
                textBoxMaxArea.Text = demand.MaxArea.ToString();
                textBoxMinRooms.Text = demand.MinRooms.ToString();
                textBoxMaxRooms.Text = demand.MaxRooms.ToString();
                textBoxMinFloor.Text = demand.MinFloor.ToString();
                textBoxMaxFloor.Text = demand.MaxFloor.ToString();
            }
            else
            {
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                textBoxMinPrice.Text = "";
                textBoxMaxPrice.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinRooms.Text = "";
                textBoxMaxRooms.Text = "";
                textBoxMinFloor.Text = "";
                textBoxMaxFloor.Text = "";
            }
        }

        private void listViewDemand_House_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDemand_House.SelectedItems.Count == 1)
            {
                DemandSet demand = listViewDemand_House.SelectedItems[0].Tag as DemandSet;
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(demand.IdAgent.ToString());
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(demand.IdClient.ToString());
                textBoxMinPrice.Text = demand.MinPrice.ToString();
                textBoxMaxPrice.Text = demand.MaxPrice.ToString();
                textBoxMinArea.Text = demand.MinArea.ToString();
                textBoxMaxArea.Text = demand.MaxArea.ToString();
                textBoxMinFloors.Text = demand.MinFloors.ToString();
                textBoxMaxFloors.Text = demand.MaxFloors.ToString();
            }
            else
            {
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                textBoxMinPrice.Text = "";
                textBoxMaxPrice.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinFloors.Text = "";
                textBoxMaxFloors.Text = "";
            }
        }

        private void listViewDemand_Land_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDemand_Land.SelectedItems.Count == 1)
            {
                DemandSet demand = listViewDemand_Land.SelectedItems[0].Tag as DemandSet;
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(demand.IdAgent.ToString());
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(demand.IdClient.ToString());
                textBoxMinPrice.Text = demand.MinPrice.ToString();
                textBoxMaxPrice.Text = demand.MaxPrice.ToString();
                textBoxMinArea.Text = demand.MinArea.ToString();
                textBoxMaxArea.Text = demand.MaxArea.ToString();
            }
            else
            {
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                textBoxMinPrice.Text = "";
                textBoxMaxPrice.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";
            }
        }
        void ShowDemandSet()
        {
            listViewDemand_Appartment.Items.Clear();
            listViewDemand_House.Items.Clear();
            listViewDemand_Land.Items.Clear();
            foreach(DemandSet demand in Program.esoft_Project.DemandSet)
            {
                if(demand.Type == 0)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demand.AgentSet.LastName+" "+demand.AgentSet.FirstName.Remove(1)+"."+demand.AgentSet.MiddleName.Remove(1)+".",
                        demand.ClientsSet.LastName+" "+demand.ClientsSet.FirstName.Remove(1)+"."+demand.ClientsSet.MiddleName.Remove(1)+".",
                        demand.MinPrice.ToString(),
                        demand.MaxPrice.ToString(),
                        demand.MinArea.ToString(),
                        demand.MaxArea.ToString(),
                        demand.MinFloor.ToString(),
                        demand.MaxFloor.ToString(),
                        demand.MinRooms.ToString(),
                        demand.MaxRooms.ToString()
                    });
                    item.Tag = demand;
                    listViewDemand_Appartment.Items.Add(item);
                }
                else if (demand.Type == 1)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demand.AgentSet.LastName+" "+demand.AgentSet.FirstName.Remove(1)+"."+demand.AgentSet.MiddleName.Remove(1)+".",
                        demand.ClientsSet.LastName+" "+demand.ClientsSet.FirstName.Remove(1)+"."+demand.ClientsSet.MiddleName.Remove(1)+".",
                        demand.MinPrice.ToString(),
                        demand.MaxPrice.ToString(),
                        demand.MinArea.ToString(),
                        demand.MaxArea.ToString(),
                        demand.MinFloors.ToString(),
                        demand.MaxFloors.ToString(),
                    });
                    item.Tag = demand;
                    listViewDemand_House.Items.Add(item);
                }
                else 
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demand.AgentSet.LastName+" "+demand.AgentSet.FirstName.Remove(1)+"."+demand.AgentSet.MiddleName.Remove(1)+".",
                        demand.ClientsSet.LastName+" "+demand.ClientsSet.FirstName.Remove(1)+"."+demand.ClientsSet.MiddleName.Remove(1)+".",
                        demand.MinPrice.ToString(),
                        demand.MaxPrice.ToString(),
                        demand.MinArea.ToString(),
                        demand.MaxArea.ToString(),
                    });
                    item.Tag = demand;
                    listViewDemand_Land.Items.Add(item);
                }
                listViewDemand_Appartment.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewDemand_House.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewDemand_Land.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    if (listViewDemand_Appartment.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewDemand_Appartment.SelectedItems[0].Tag as DemandSet;
                        demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                        demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                        if (textBoxMinPrice.Text != "")
                            demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                        else demand.MinPrice = null;
                        if (textBoxMaxPrice.Text != "")
                            demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                        else demand.MaxPrice = null;
                        if (demand.MaxPrice < demand.MinPrice)
                            throw new ApplicationException("цена");
                        if (textBoxMinArea.Text != "")
                            demand.MinArea = Convert.ToInt32(textBoxMinArea.Text);
                        else demand.MinArea = null;
                        if (textBoxMaxArea.Text != "")
                            demand.MaxArea = Convert.ToInt32(textBoxMaxArea.Text);
                        else demand.MaxArea = null;
                        if (demand.MaxArea < demand.MinArea) 
                            throw new ApplicationException("площадь");
                        if (textBoxMinFloor.Text != "")
                            demand.MinFloor = Convert.ToInt32(textBoxMinFloor.Text);
                        else demand.MinFloor = null;
                        if (textBoxMaxFloor.Text != "")
                            demand.MaxFloor = Convert.ToInt32(textBoxMaxFloor.Text);
                        else demand.MaxFloor = null;
                        if (demand.MaxFloor < demand.MinFloor) 
                            throw new Exception("этаж");
                        if (textBoxMinRooms.Text != "")
                            demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                        else demand.MinRooms = null;
                        if (textBoxMaxRooms.Text != "")
                            demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                        else demand.MaxRooms = null;
                        if (demand.MaxRooms < demand.MinRooms) 
                            throw new AggregateException("количество комнат");
                        Program.esoft_Project.SaveChanges();
                        ShowDemandSet();
                    }
                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    if (listViewDemand_House.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewDemand_House.SelectedItems[0].Tag as DemandSet;
                        demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                        demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                        if (textBoxMinPrice.Text != "")
                            demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                        else demand.MinPrice = null;
                        if (textBoxMaxPrice.Text != "")
                            demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                        else demand.MaxPrice = null;
                        if (demand.MaxPrice < demand.MinPrice)
                            throw new ApplicationException("цена");
                        if (textBoxMinArea.Text != "")
                            demand.MinArea = Convert.ToInt32(textBoxMinArea.Text);
                        else demand.MinArea = null;
                        if (textBoxMaxArea.Text != "")
                            demand.MaxArea = Convert.ToInt32(textBoxMaxArea.Text);
                        else demand.MaxArea = null;
                        if (demand.MaxArea < demand.MinArea)
                            throw new ApplicationException("площадь");
                        if (textBoxMinFloors.Text != "")
                            demand.MinFloors = Convert.ToInt32(textBoxMinFloors.Text);
                        else demand.MinFloors = null;
                        if (textBoxMaxFloors.Text != "")
                            demand.MaxFloors = Convert.ToInt32(textBoxMaxFloors.Text);
                        else demand.MaxFloors = null;
                        if (demand.MaxArea < demand.MinArea) 
                            throw new ApplicationException("этажность");
                        Program.esoft_Project.SaveChanges();
                        ShowDemandSet();
                    }
                }
                else
                {
                    if (listViewDemand_Land.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewDemand_Land.SelectedItems[0].Tag as DemandSet;
                        if (textBoxMinPrice.Text != "")
                            demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                        else demand.MinPrice = null;
                        if (textBoxMaxPrice.Text != "")
                            demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                        else demand.MaxPrice = null;
                        if (demand.MaxPrice < demand.MinPrice)
                            throw new ApplicationException("цена");
                        if (textBoxMinArea.Text != "")
                            demand.MinArea = Convert.ToInt32(textBoxMinArea.Text);
                        else demand.MinArea = null;
                        if (textBoxMaxArea.Text != "")
                            demand.MaxArea = Convert.ToInt32(textBoxMaxArea.Text);
                        else demand.MaxArea = null;
                        if (demand.MaxArea < demand.MinArea)
                            throw new ApplicationException("площадь");
                        Program.esoft_Project.SaveChanges();
                        ShowDemandSet();
                    }
                }
            }
            catch (ApplicationException ex) { MessageBox.Show("Максимальная " + ex.Message + " не может быть меньше минимальной", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (AggregateException ex) { MessageBox.Show("Максимальное " + ex.Message + " не может быть меньше минимального", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (Exception ex) { MessageBox.Show("Максимальный " + ex.Message + " не может быть меньше минимального", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    if (listViewDemand_Appartment.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewDemand_Appartment.SelectedItems[0].Tag as DemandSet;
                        Program.esoft_Project.DemandSet.Remove(demand);
                        Program.esoft_Project.SaveChanges();
                    }
                    comboBoxAgents.SelectedItem = null;
                    comboBoxClients.SelectedItem = null;
                    textBoxMinPrice.Text = "";
                    textBoxMaxPrice.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxArea.Text = "";
                    textBoxMinRooms.Text = "";
                    textBoxMaxRooms.Text = "";
                    textBoxMinFloor.Text = "";
                    textBoxMaxFloor.Text = "";
                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    if (listViewDemand_House.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewDemand_House.SelectedItems[0].Tag as DemandSet;
                        Program.esoft_Project.DemandSet.Remove(demand);
                        Program.esoft_Project.SaveChanges();
                    }
                    comboBoxAgents.SelectedItem = null;
                    comboBoxClients.SelectedItem = null;
                    textBoxMinPrice.Text = "";
                    textBoxMaxPrice.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxArea.Text = "";
                    textBoxMinFloors.Text = "";
                    textBoxMaxFloors.Text = "";
                }
                else
                {
                    if (listViewDemand_Land.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewDemand_Land.SelectedItems[0].Tag as DemandSet;
                        Program.esoft_Project.DemandSet.Remove(demand);
                        Program.esoft_Project.SaveChanges();
                    }
                    comboBoxAgents.SelectedItem = null;
                    comboBoxClients.SelectedItem = null;
                    textBoxMinPrice.Text = "";
                    textBoxMaxPrice.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxArea.Text = "";
                }
                ShowDemandSet();
            }
            catch { MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void buttonDel_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxMaxArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8) //цифры, клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
