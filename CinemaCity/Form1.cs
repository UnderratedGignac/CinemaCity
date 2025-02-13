using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
namespace CinemaCity
{
    public partial class Form1 : Form
    {
        List<Button> buttons = new List<Button>();
        int c = 0,counter=0;
        int ticketprice=0;
        List<Button> choosed = new List<Button>();
        DataTable dt = new DataTable();
        Connection conn = new Connection();
        List<Button> list = new List<Button>();
        int count = 0,counts=0;
        List<string> selected_seats = new List<string>();
        int id;
        TimeSpan ts = new TimeSpan(22, 0, 0);
        TimeSpan currentTime = DateTime.Now.TimeOfDay;
        TimeSpan six_Thitrty = new TimeSpan(18, 30, 0);
        int[] tabcount = new int[6] { 0, 0, 0, 0, 0, 0 };
        TabPage tabpage;
        int id_row_to_delete;
        public Form1()
        {
            InitializeComponent();
            if (ts > currentTime)
            {
                if (currentTime < six_Thitrty)
                {
                    conn.openCon();
                    dt.Load(conn.selectQ("SELECT * FROM Movie"));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String[] temp = new string[10];
                        for (int j = 0; j < temp.Length; j++)
                        {
                            temp[j] = dt.Rows[i][j].ToString();

                        }
                        ListViewItem item = new ListViewItem(temp[0]);
                        item.SubItems.Add(temp[6]);
                        item.SubItems.Add(temp[2]);
                        item.SubItems.Add(temp[1]);
                        listView2.Items.Add(item);

                    }
                    conn.closeCon();
                }
                else
                {

                    conn.openCon();
                    dt.Load(conn.selectQ("SELECT * FROM Movie where Movie.timeofstreaming = '9:30 pm'; "));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String[] temp = new string[10];
                        for (int j = 0; j < temp.Length; j++)
                        {
                            temp[j] = dt.Rows[i][j].ToString();

                        }
                        ListViewItem item = new ListViewItem(temp[0]);
                        item.SubItems.Add(temp[6]);
                        item.SubItems.Add(temp[2]);
                        item.SubItems.Add(temp[1]);
                        listView2.Items.Add(item);

                    }
                    conn.closeCon();
                }
            }
            else
            {
                MessageBox.Show("We  are closed for tonight,Come back tomorrow.");
                seating.Enabled = false;
            }
        }
        private void login_(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void reserved(object sender, EventArgs e)
        {
            Button b=sender as Button;
            if (b.BackColor!=Color.DarkGray)
            {
                int i = 0;
                bool alreadyclicked = false;
                while (i < counts)
                {
                    try
                    {
                        if (b.Name == selected_seats[i])
                        {
                            alreadyclicked = true;
                        }
                        i++;
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
                if (alreadyclicked == false)
                {
                    b.BackColor = Color.Green;
                    buttons.Add(b);
                    choosed.Add(b);
                    c = c + 1;
                    counter++;
                    selected_seats.Add(b.Name);
                    //current_selected_seats[counts] = b.Text;
                    counts++;
                }
                if (alreadyclicked)
                {
                    choosed.Remove(b);
                    buttons.Remove(b);
                    b.BackColor = Color.White;
                    //Button b2 = new Button();
                    //b2.Text = "";
                    //c = c - 1;
                    //choosed_seats[c] =b2;
                    counter--;
                    selected_seats.Remove(b.Name);
                    counts--;
                    //current_selected_seats[counts] = "";
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            room_name.Text = "";
            movie_duration.Text = "";
            ticket_Price.Text = "";
            DataTable dt7 = new DataTable();
            conn.openCon();
            dt7.Load(conn.selectQ("select * from movie"));
            dataGridView1.DataSource = dt7;
            conn.closeCon();
        }

        private void seating_Click(object sender, EventArgs e)
        {
            counter = 0;
            string[] cinema_rows = new string[7] { "A", "B", "C", "D", "E", "F", "G" };
            int index = 0;
            if (listView2.SelectedItems.Count > 0)
            {
                index = listView2.SelectedIndices[0];
            }
            if (listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a Movie");
            }
            else
            {
                if (room_name.Text == "1" & listView2.Items[index].SubItems[1].Text == "6:30 pm")
                {

                    tabControl1.SelectedTab = tabPage3;
                    if (tabcount[0] == 0)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            for (int i = 0; i < 7 + j; i++)
                            {

                                Button button = new Button();
                                button.Location = new System.Drawing.Point(270 + (i * 60) - (j * 30), 100 + (j * 50));
                                button.Name = cinema_rows[j] + i ;
                                button.Size = new System.Drawing.Size(46, 38);
                                button.TabIndex = 0;
                                button.Text = cinema_rows[j] + i;
                                button.UseVisualStyleBackColor = true;
                                button.Click += new System.EventHandler(this.reserved);
                                tabPage3.Controls.Add(button);
                                list.Add(button);
                            }
                        }
                        tabcount[0] = 1;
                    }
                }
                else if (room_name.Text == "2" & listView2.Items[index].SubItems[1].Text == "6:30 pm")
                {
                    tabControl1.SelectedTab = tabPage6;
                    if (tabcount[1] == 0)
                    {
                        int x = 100;
                        for (int j = 0; j < 5; j++)
                        {
                            x = 20;
                            for (int i = 0; i < 12; i++)
                            {

                                x = x + 50;
                                if (i % 3 == 0 & x != 0)
                                {
                                    x = x + 40;
                                }

                                Button button2 = new Button();
                                button2.Location = new System.Drawing.Point(x, 100 + (j * 50));
                                button2.Name = cinema_rows[j] + i;
                                button2.Size = new System.Drawing.Size(46, 38);
                                button2.TabIndex = 0;
                                button2.Text = cinema_rows[j] + i;
                                button2.UseVisualStyleBackColor = true;
                                button2.Click += new System.EventHandler(this.reserved);
                                tabPage6.Controls.Add(button2);
                                list.Add(button2);
                            }
                        }tabcount[1] = 1;
                    }

                }
                else if (room_name.Text == "3" & listView2.Items[index].SubItems[1].Text == "6:30 pm")
                {
                    tabControl1.SelectedTab = tabPage7;
                    int x = 100;
                    if (tabcount[2] == 0)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            x = 20;
                            for (int i = 0; i < 12; i++)
                            {

                                x = x + 50;
                                if (i % 4 == 0 & x != 0)
                                {
                                    x = x + 40;
                                }

                                Button button2 = new Button();
                                button2.Location = new System.Drawing.Point(x, 100 + (j * 50));
                                button2.Name = cinema_rows[j] + i ;
                                button2.Size = new System.Drawing.Size(46, 38);
                                button2.TabIndex = 0;
                                button2.Text = cinema_rows[j] + i;
                                button2.UseVisualStyleBackColor = true;
                                button2.Click += new System.EventHandler(this.reserved);
                                tabPage7.Controls.Add(button2);
                                list.Add(button2);
                            }
                        }
                        tabcount[2] = 1;
                    }
                }
                if (room_name.Text == "1" & listView2.Items[index].SubItems[1].Text == "9:30 pm")
                {
                    tabControl1.SelectedTab = tabPage9;
                    if (tabcount[3] == 0) { 
                        for (int j = 0; j < 5; j++)
                        {
                            for (int i = 0; i < 7 + j; i++)
                            {
                                Button button = new Button();
                                button.Location = new System.Drawing.Point(270 + (i * 60) - (j * 30), 100 + (j * 50));
                                button.Name = cinema_rows[j] + i ;
                                button.Size = new System.Drawing.Size(46, 38);
                                button.TabIndex = 0;
                                button.Text = cinema_rows[j] + i;
                                button.UseVisualStyleBackColor = true;
                                button.Click += new System.EventHandler(this.reserved);
                                tabPage9.Controls.Add(button);
                                list.Add(button);
                            }
                        }
                        tabcount[3]= 1;
                    }
                }

                else if (room_name.Text == "2" & listView2.Items[index].SubItems[1].Text == "9:30 pm")
                {
                    tabControl1.SelectedTab = tabPage8;
                    int x = 100;
                    if (tabcount[4] == 0)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            x = 20;
                            for (int i = 0; i < 12; i++)
                            {

                                x = x + 50;
                                if (i % 3 == 0 & x != 0)
                                {
                                    x = x + 40;
                                }

                                Button button2 = new Button();
                                button2.Location = new System.Drawing.Point(x, 100 + (j * 50));
                                button2.Name = cinema_rows[j] + i ;
                                button2.Size = new System.Drawing.Size(46, 38);
                                button2.TabIndex = 0;
                                button2.Text = cinema_rows[j] + i;
                                button2.UseVisualStyleBackColor = true;
                                button2.Click += new System.EventHandler(this.reserved);
                                tabPage8.Controls.Add(button2);
                                list.Add(button2);
                            }
                        }tabcount[4] = 1;
                    }

                }
                else if (room_name.Text == "3" & listView2.Items[index].SubItems[1].Text == "9:30 pm")
                {
                    tabControl1.SelectedTab = tabPage10;
                    int x = 100;
                    if (tabcount[5] == 0)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            x = 20;
                            for (int i = 0; i < 12; i++)
                            {

                                x = x + 50;
                                if (i % 4 == 0 & x != 0)
                                {
                                    x = x + 40;
                                }

                                Button button2 = new Button();
                                button2.Location = new System.Drawing.Point(x, 100 + (j * 50));
                                button2.Name = cinema_rows[j] + i ;
                                button2.Size = new System.Drawing.Size(46, 38);
                                button2.TabIndex = 0;
                                button2.Text = cinema_rows[j] + i;
                                button2.UseVisualStyleBackColor = true;
                                button2.Click += new System.EventHandler(this.reserved);
                                tabPage10.Controls.Add(button2);
                                list.Add(button2);
                            }
                        }
                        tabcount[5] = 1;
                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            tabpage = tabControl1.SelectedTab;
            if (selected_seats.Count == 0)
            {
                MessageBox.Show("Choose atleast one seat");
            }
            else
            {
                conn.openCon();
                tabControl1.SelectedTab = tabPage4;
                listView1.Items.Clear();
                final_price.Text = "" + (selected_seats.Count * ticketprice + (ticketprice * selected_seats.Count) * 10 / 100) + " $";
                ticket_price_final.Text = "" + ticketprice;
                tax.Text = "" + (ticketprice * selected_seats.Count * 10 / 100);
                quantity_final.Text = "" + selected_seats.Count;
                DataTable dt1 = new DataTable();
                dt1.Load(conn.selectQ("SELECT movie.name,movie.class,movie.description,movie.duration,movie.timeofstreaming,Movie.picture,room.name,room.capacity FROM Movie,room where movie.id =" + id + "and room.id=room_id;"));
                string[] temp = new string[dt1.Columns.Count];
                for (
                    int i = 0; i < temp.Length; i++)
                {
                    temp[i] = dt1.Rows[0][i].ToString();
                }
                for (int i = 0; i < selected_seats.Count; i++)
                {
                        ListViewItem temp2 = new ListViewItem(temp[0]);
                        temp2.SubItems.Add(temp[6]);
                        temp2.SubItems.Add(temp[1]);
                        temp2.SubItems.Add(selected_seats[i]);
                        temp2.SubItems.Add(temp[4]);
                        listView1.Items.Add(temp2);
                    
                }
                conn.closeCon();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (username.Text =="elie" & Password.Text == "elie")
            {
                tabControl1.SelectedTab=tabPage5;
                conn.openCon();
                DataTable dataTable = new DataTable();
                dataTable.Load(conn.selectQ("select * from room where available =1"));
                room_name_add.DataSource = dataTable;
                room_name_add.ValueMember = "id";
                room_name_add.DisplayMember = "name";
                time_of_streaming_add.DataSource = dataTable;
                time_of_streaming_add.ValueMember = "id";
                time_of_streaming_add.DisplayMember = "time";
                conn.closeCon();
            }
            else
            {
                MessageBox.Show("incorrect informations");
            }
            username.Clear();
            Password.Clear();
        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            conn.openCon();
            if (add_movie_name.Text == "" |
                Class_name_add.Text=="" |
                Movie_author_add.Text =="" |
                Movie_description_add.Text ==""|
                Movie_duration_add.Text ==""|
                movie_picture_add.Text==""|
                room_name_add.Text==""|
                time_of_streaming_add.Text==""|
                type_add.Text == "")
            {
                MessageBox.Show("Add missing items ");

            }
            else
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(conn.selectQ("select * from movie;"));
                int a1 =Convert.ToInt16(dataTable.Rows[dataTable.Rows.Count-1][1]);
                int id = a1+1;
                String a = "insert into movie values("+
                    "'"+ add_movie_name.Text +"'"
                    + "," + id + "," 
                    + "'" + Class_name_add.Text+ "'," +
                    "'" + movie_picture_add.Text+"'," +
                    "'" +type_add.Text+"'," + "" +
                    Convert.ToInt16( room_name_add.Text)+ 
                    ",'" + time_of_streaming_add.Text+"'," 
                    + "'" + Movie_duration_add.Text+"'," + "'" 
                    +Movie_description_add.Text+"'" +",'"
                    + Movie_author_add.Text+"');"  ;
                    conn.modifyQ(a);
                    if (time_of_streaming_add.Text == "6:30 pm")
                    {
                        conn.modifyQ("update room set available =0 where id = " +room_name_add.Text);
                    }
                    else
                    {
                    int ids = Convert.ToInt32(room_name_add.Text) + 3;
                        conn.modifyQ("update room set available =0 where id = " +ids);
                    }
                add_movie_name.Text = "";
                Class_name_add.Text = "";
                Movie_author_add.Text ="";
                Movie_description_add.Text = "";
                Movie_duration_add.Text = "";
                movie_picture_add.Text = "";
                room_name_add.Text = "";
                time_of_streaming_add.Text = "";
                type_add.Text = "";
                DataTable dt3 = new DataTable();
                dt3.Load(conn.selectQ("SELECT * FROM Movie"));
                dataGridView1.DataSource = dt3;
                MessageBox.Show("added");
                listView2.Items.Clear();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    String[] temp = new string[10];
                    for (int j = 0; j < temp.Length; j++)
                    {
                        temp[j] = dt3.Rows[i][j].ToString();

                    }
                    ListViewItem item = new ListViewItem(temp[0]);
                    item.SubItems.Add(temp[6]);
                    item.SubItems.Add(temp[2]);
                    item.SubItems.Add(temp[1]);
                    listView2.Items.Add(item);

                }
                DataTable dataTable1 = new DataTable();
                dataTable1.Load(conn.selectQ("select * from room where available =1"));
                room_name_add.DataSource = dataTable1;
                room_name_add.ValueMember = "id";
                room_name_add.DisplayMember = "name";
                time_of_streaming_add.DataSource = dataTable1;
                time_of_streaming_add.ValueMember = "id";
                time_of_streaming_add.DisplayMember = "time";

                conn.closeCon();
            }

        }

        private void Remove_movie_Admin_Click(object sender, EventArgs e) 
        {
            tabControl1.SelectedTab = tabPage11;
        }
        //{
        //    conn.openCon();
        //    DataTable dt1 = new DataTable();
        //    dt1.Load(conn.selectQ("select room_id from movie where id =" + Convert.ToInt16(remove_movie.Text)));
        //    try
        //    {
        //        conn.modifyQ("delete from Movie where id=" + Convert.ToInt16(remove_movie.Text));
        //        conn.modifyQ("update room set available =1 where id = " +Convert.ToInt16( dt1.Rows[0][0].ToString()));
        //        MessageBox.Show("deleted");
        //        remove_movie.Text = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("no movie found");
        //    }

        //    DataTable dataTable = new DataTable();
        //    dataTable.Load(conn.selectQ("select * from room where available =1"));
        //    room_name_add.DataSource = dataTable;
        //    room_name_add.ValueMember = "id";
        //    room_name_add.DisplayMember = "name";
        //    time_of_streaming_add.DataSource = dataTable;
        //    time_of_streaming_add.ValueMember = "id";
        //    time_of_streaming_add.DisplayMember = "time";
        //    DataTable dt3 = new DataTable();
        //    dt3.Load(conn.selectQ("SELECT * FROM Movie"));
        //    listView2.Items.Clear();
        //    for (int i = 0; i < dt3.Rows.Count; i++)
        //    {
        //        String[] temp = new string[10];
        //        for (int j = 0; j < dt3.Columns.Count; j++)
        //        {
        //            temp[j] = dt3.Rows[i][j].ToString();

        //        }
        //        ListViewItem item = new ListViewItem(temp[0]);
        //        item.SubItems.Add(temp[6]);
        //        item.SubItems.Add(temp[2]);
        //        item.SubItems.Add(temp[1]);
        //        listView2.Items.Add(item);

        //    }
        //    conn.closeCon();

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void back(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabpage;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
            username.Clear();
            Password.Clear();
        }

        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //    MessageBox.Show(""+e.RowIndex);
        //    //update column data grid view
        //    conn.modifyQ("update movie set " + " where id = " + Convert.ToInt16(dataGridView1.Rows[e.RowIndex].Cells[1])) ;

        //}
        private void remove_siu(object sender, DataGridViewCellEventArgs e)
        {
            try {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    id_row_to_delete = Convert.ToInt16(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                }
            }
            catch (Exception ex)
            { 
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            conn.openCon();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                int id_room_row_selected = Convert.ToInt16(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                dt1.Load(conn.selectQ("select room_id from movie where id =" + id_room_row_selected));
                try
                {
                    conn.modifyQ("delete from Movie where id=" + Convert.ToInt16(id_row_to_delete)+" ;");
                    conn.modifyQ("update room set available =1 where id = " + id_room_row_selected+" ;");
                    MessageBox.Show("deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("no movie found");
                }

                DataTable dataTable = new DataTable();
                dataTable.Load(conn.selectQ("select * from room where available =1"));
                room_name_add.DataSource = dataTable;
                room_name_add.ValueMember = "id";
                room_name_add.DisplayMember = "name";
                time_of_streaming_add.DataSource = dataTable;
                time_of_streaming_add.ValueMember = "id";
                time_of_streaming_add.DisplayMember = "time";
                DataTable dt3 = new DataTable();
                dt3.Load(conn.selectQ("SELECT * FROM Movie"));
                listView2.Items.Clear();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    String[] temp = new string[10];
                    for (int j = 0; j < dt3.Columns.Count; j++)
                    {
                        temp[j] = dt3.Rows[i][j].ToString();

                    }
                    ListViewItem item = new ListViewItem(temp[0]);
                    item.SubItems.Add(temp[6]);
                    item.SubItems.Add(temp[2]);
                    item.SubItems.Add(temp[1]);
                    listView2.Items.Add(item);

                }DataTable dt4 = new DataTable();
                dt4.Load(conn.selectQ("select * from movie"));
                dataGridView1.DataSource = dt4;
                conn.closeCon();
            }
            else
            {
                MessageBox.Show("No rows selected");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            conn.openCon();
            if(e.ColumnIndex==1 | e.ColumnIndex==5 | e.ColumnIndex == 6)
            {
                MessageBox.Show("Can t modify these information");
                DataTable dt5 = new DataTable();
                dt5.Load(conn.selectQ("select * from movie"));
                dataGridView1.DataSource = dt5;

            } else
            if(e.ColumnIndex != 1 | e.ColumnIndex != 5 | e.ColumnIndex !=6)
            {
                DataTable dt5 = new DataTable();
                dt5.Load(conn.selectQ("select * from movie"));
                string a = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                int id = Convert.ToInt16( dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                conn.modifyQ("update movie set " + dt5.Columns[e.ColumnIndex].ColumnName + " = '" + a+"' where id ="+id+";");
                dt5.Clear();
                dt5.Load(conn.selectQ("select * from movie"));
                dataGridView1.DataSource=dt5;
            }
            conn.closeCon();

            }

        private void button14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].BackColor == Color.Green)
                {
                    list[i].BackColor = Color.White;
                }
            }
            choosed.Clear();
            selected_seats.Clear();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            final_price.Text = "";
            movie_description.Clear();
            room_name.Text="";
            movie_duration.Text="";
            ticket_Price.Text="";
            listView1.Items.Clear();

            for (int k = 0; k < list.Count; k++)
                {
                if (list[k].BackColor == Color.Green)
                {
                    list[k].BackColor=Color.DarkGray;
                    list[k].Enabled=false;
                }
            }
            for (int j = selected_seats.Count-1; j >=0; j--)
            {
                selected_seats.RemoveAt(j);
            }
            counts = 0;
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.openCon();
            int index=0;
            if (listView2.SelectedItems.Count > 0)
            {
                index = listView2.SelectedIndices[0];
            }
            int selectedItem =Convert.ToInt16( listView2.Items[index].SubItems[3].Text);
            id =selectedItem;
            DataTable dt1 = new DataTable();
            dt1.Load(conn.selectQ("SELECT movie.name,movie.class,movie.description,movie.duration,movie.timeofstreaming,Movie.picture,room.name,room.capacity FROM Movie,room where movie.id ="  + id+ "and room.id=room_id;"));
            string []temp=new string [dt1.Columns.Count];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = dt1.Rows[0][i].ToString(); 
            }
            label7.Text = temp[0];
            int x = (ClientSize.Width - label7.Width) / 2 +60;
            label7.Location = new System.Drawing.Point(x, 3);
            room_name.Text = temp[6];
            movie_duration.Text = temp[3];
            movie_description.Text = temp[2];
            string class_name = temp[1];
            this.pictureBoxcarousel.Image = System.Drawing.Image.FromFile("C:\\Users\\Anthony\\Desktop\\CinemaCIty\\CinemaCity\\CinemaCity\\"+temp[5]);
            if (class_name == "Vip")
            {
                ticketprice = 25;
            }
            else if (class_name == "4dx")
                ticketprice = 20;
            else
                ticketprice = 15;
            ticket_Price.Text = ticketprice + " $";
            conn.closeCon();
        }
    }
}