using CelotMClient.Manager;
using CelotMClient.NMSStructure;
using CelotMClient.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.CustomForm
{
    public partial class RouterCommand : Form
    {
        private NMSReportCommand nmsReportCommand;
        private RouterCommandManager routerCommandManager;

        private bool isMouseDown = false;
        private int xLast;
        private int yLast;

        public RouterCommand(NMSReportCommand nmsReportCommand)
        {
            InitializeComponent();
            this.nmsReportCommand = nmsReportCommand;
            this.routerCommandManager = new RouterCommandManager();
        }

        private void RouterCommand_Load(object sender, EventArgs e)
        {
            this.initControll();
            CelotApplication.Instance().ViewTransferEnable= true;
        }

        private void initControll()
        {
            this.routerNameLabel.Text = this.nmsReportCommand.Device.Name;
            this.routerIpLabel.Text = String.Format("{0}", this.nmsReportCommand.Device.RouterIp);
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (!CelotApplication.Instance().ViewTransferEnable)
            {
                MessageBox.Show(String.Format("현재 {0} 작업이 진행중입니다.", CelotApplication.Instance().JobType), "작업 진행중");
                return;
            }
            Close();
        }

        private void pinaBtn_Click(object sender, EventArgs e)
        {
            if (!CelotApplication.Instance().ViewTransferEnable)
            {
                MessageBox.Show(String.Format("현재 {0} 작업이 진행중입니다.", CelotApplication.Instance().JobType), "작업 진행중");
                return;
            }

            CelotApplication.Instance().ViewTransferEnable = false;
            CelotApplication.Instance().JobType = "Ping Test";

            this.progressBar1.Visible = false;
            resultTextBox.Text = "";

            Thread t = new Thread(new ParameterizedThreadStart(
                delegate(object o)
                {
                    RouterCommandManager routerCommand = new RouterCommandManager();
                    int trycount = (int)o;
                    resultTextBox.Invoke(new Action(delegate()
                    {
                        resultTextBox.Text = "";
                        resultTextBox.AppendText(String.Format("Ping {0} 32바이트 데이터 사용\n", this.nmsReportCommand.Device.RouterIp));
                    }));
                    for (int i = 0; i < 3; i++)
                    {
                        String message = null;
                        routerCommand.Ping(this.nmsReportCommand.Device.RouterIp, out message);
                        resultTextBox.Invoke(new Action(delegate()
                       {
                           resultTextBox.AppendText(message + "\n");
                       }));
                        Thread.Sleep(200);
                    }
                    CelotApplication.Instance().ViewTransferEnable = true;
                    CelotApplication.Instance().JobType = "";
                }));
            t.Start(3);
        }

        private void rawDataBtn_Click(object sender, EventArgs e)
        {
            if (!CelotApplication.Instance().ViewTransferEnable)
            {
                MessageBox.Show(String.Format("현재 {0} 작업이 진행중입니다.", CelotApplication.Instance().JobType), "작업 진행중");
                return;
            }
            CelotApplication.Instance().ViewTransferEnable = false;
            CelotApplication.Instance().JobType = "Creation Of Packet Raw Data ";
            this.progressBar1.Visible = false;
            resultTextBox.Text = "";

              Thread t = new Thread(new ParameterizedThreadStart(
                delegate(object o)
                {
                    RouterCommandManager routerCommand = new RouterCommandManager();
                    int trycount = (int)o;
                    resultTextBox.Invoke(new Action(delegate()
                    {
                        resultTextBox.Text = "";
                        resultTextBox.AppendText( String.Format(">> Phone Number[{0}], RouterIP[{1}] 의 RAW Data 를 추출합니다",
                            this.nmsReportCommand.Device.PhoneNumber,
                            this.nmsReportCommand.Device.RouterIp
                            ));
                        resultTextBox.AppendText("\n");
                       
                    }));

                    int session_id = (int)nmsReportCommand.Device.PhoneNumber;
                    DateTime date = DateTime.Now.ToLocalTime().AddMonths(-6);
                    var span = (date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
                    int timestamp = (int)span.TotalSeconds;

                    string query = @"select * from packet_log WHERE  1 =1 ";
                    if (session_id != 0)
                        query += @" AND SessionId = " + session_id;

                    if (timestamp != 0)
                        query += @" AND PacketLogRegDate >  " + timestamp;

                    MySqlConnection con = DatabaseManager.Instance().GetConnection();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = con;
                    command.CommandText = query;
                    MySqlDataReader reader = command.ExecuteReader();
                   
                    if (reader.HasRows)
                    {
                        string sDirPath;
                        sDirPath = Application.StartupPath + "\\LOGS_RAWDATA";
                        DirectoryInfo di = new DirectoryInfo(sDirPath);
                        if (di.Exists == false)
                        {
                            di.Create();
                        }

                        byte[] rawPacketBuffer = new byte[815];
                        StreamWriter writer = File.CreateText( Path.Combine(sDirPath, String.Format("session_{0}_{1}_rawdata_log.txt",
                                this.nmsReportCommand.Device.PhoneNumber,
                                CelotUtility.GetCurrentTimeString("yyMMddHHmmss"))
                            )
                        ); 

                        while (reader.Read()) 
                        {
                            if (!DBNull.Value.Equals(reader["RawPacket"]))
                            {
                                reader.GetBytes(reader.GetOrdinal("RawPacket"), 0, rawPacketBuffer, 0, 815);
                                writer.WriteLine(String.Format("SessionID[{0}],RegDate[{1}]", 
                                    reader["SessionId"],
                                    CelotUtility.UnixTimeStampToDateString(Convert.ToInt32(reader["PacketLogRegDate"]))));
                                //HEX String
                                writer.WriteLine(BitConverter.ToString(rawPacketBuffer).Replace("-", " "));
                                //UNICODE String
                                //writer.WriteLine(System.Text.Encoding.ASCII.GetString(rawPacketBuffer));
                                writer.WriteLine("\n\n");
                            }
                        }
                        writer.Close();
                    }
                    else
                    {
                        resultTextBox.Invoke(new Action(delegate()
                        {
                            resultTextBox.AppendText(String.Format(">> Raw Data가 존재하지 않습니다"));

                        }));
                    }
                    
                    resultTextBox.Invoke(new Action(delegate()
                    {
                        resultTextBox.AppendText(String.Format(">> Raw Data 파일 생성이 완료되었습니다"));

                    }));
                    CelotApplication.Instance().ViewTransferEnable = true;
                    CelotApplication.Instance().JobType = "";
                }));
            t.Start(3);

            
        }

        private void httpBtn_Click(object sender, EventArgs e)
        {
            this.progressBar1.Visible = false;
            String target = String.Format("http://{0}", this.nmsReportCommand.Device.RouterIp);
            this.routerCommandManager.HttpOpen(target);
        }

        private void firmDownBtn_Click(object sender, EventArgs e)
        {
            if (!CelotApplication.Instance().ViewTransferEnable)
            {
                MessageBox.Show(String.Format("현재 {0} 작업이 진행중입니다.", CelotApplication.Instance().JobType), "작업 진행중");
                return;
            }
            this.Close();
            CelotApplication.Instance().ViewIntent = new ViewIntent { Data = this.nmsReportCommand.Device.RouterIp };
            CelotApplication.Instance().MainForm.MoveApplicatioinView();
        }

        private void routerResetBtn_Click(object sender, EventArgs e)
        {
            if (!CelotApplication.Instance().ViewTransferEnable)
            {
                MessageBox.Show(String.Format("현재 {0} 작업이 진행중입니다.", CelotApplication.Instance().JobType), "작업 진행중");
                return;
            }
            CelotApplication.Instance().ViewTransferEnable = false;
            CelotApplication.Instance().JobType = "Router Reset ";
            resultTextBox.Text = "";

            int curMileSecond = CelotUtility.GetCurrentUnixTimeStamp();
            Thread t = new Thread(new ThreadStart(
                delegate()
                {
                    MySqlConnection con = null;
                    MySqlCommand command = null;

                    try
                    {
                        RouterCommandManager routerCommand = new RouterCommandManager();
                        resultTextBox.Invoke(new Action(delegate()
                        {
                            resultTextBox.Text = "";
                            resultTextBox.AppendText(
                                String.Format("[CONNECTING] Router : [{0}]:[{2}]:[{2}]",
                                nmsReportCommand.Device.RouterIp, ApplicationConfig.Instance().RemotePort, nmsReportCommand.Device.SecuCode));
                        }));
                        bool result = routerCommand.RouterReset(
                            nmsReportCommand.Device.SecuCode,
                            this.nmsReportCommand.Device.RouterIp,
                            ApplicationConfig.Instance().RemotePort
                        );
                        resultTextBox.Invoke(new Action(delegate()
                        {
                            resultTextBox.AppendText(routerCommand.Message);
                        }));

                        if (!result)
                        {
                            return; 
                        }

                        con = DatabaseManager.Instance().GetConnection();
                        string sql = String.Format("UPDATE device SET ResetTime = {0} WHERE SerialNo = {1}",
                            curMileSecond, this.nmsReportCommand.Device.SerialNo);
                        command = new MySqlCommand(sql, con);
                        command.ExecuteNonQuery();
                        resultTextBox.Invoke(new Action(delegate()
                        {
                            resultTextBox.AppendText("Database update OK");
                        }));
                    }
                    catch (Exception ee)
                    {
                        resultTextBox.Invoke(new Action(delegate()
                        {
                            resultTextBox.AppendText(ee.Message);
                        }));
                    }
                    finally
                    {
                        CelotApplication.Instance().ViewTransferEnable = true;
                        CelotApplication.Instance().JobType = "";
                        if (con != null)
                            con.Close();
                    }
                }));
            t.Start();
       
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            yLast = e.Y;
            xLast = e.X;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int newY = this.Top + (e.Y - yLast);
                int newX = this.Left + (e.X - xLast);
                this.Location = new Point(newX, newY);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            xLast = e.X;
            yLast = e.Y;
        }

        #region not supported

        //Label d_la;
        //FlowLayoutPanel d_fLayout;
        //ComboBox d_hc;
        //ComboBox d_mc;
        //CheckBox d_dc;
        //ComboBox d_wc;

        //FlowLayoutPanel d_fLayout2;
        //ComboBox h_hc;
        //bool isD_fLayout2Add = false;
        //bool isD_fLayoutAdd = false;

        //bool isCreateOptionControll = false;
        //private string p1;
        //private int p2;
        //private string p3;
        //private string p4;
        //private string p5;
        //private uint p6;

        //private void timeConfigCombo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ComboBox combo = (ComboBox)sender;
        //    if (!isCreateOptionControll)
        //    {
        //        d_la = new Label();
        //        d_la.Text = "날짜 주기 설정";
        //        d_la.BackColor = Color.FromArgb(255, 64, 64, 64);
        //        d_la.ForeColor = Color.FromArgb(255, 255, 255, 255);
        //        d_la.Dock = DockStyle.Fill;
        //        d_la.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //        resetOptionTable.Controls.Add(d_la, 0, 2);

        //        d_fLayout = new FlowLayoutPanel();
        //        d_fLayout.Dock = DockStyle.Fill;

        //        Label t = new Label();
        //        t.Text = "시간";
        //        t.ForeColor = Color.FromArgb(255, 255, 255, 255);

        //        d_hc = new ComboBox();
        //        d_hc.DropDownStyle = ComboBoxStyle.DropDownList;
        //        string[] times = new string[24];
        //        for (int i = 0; i < 24; i++)
        //        {
        //            times[i] = i.ToString();
        //        }
        //        d_hc.Items.AddRange(times);
        //        d_hc.Size = new Size(40, 10);
        //        d_hc.SelectedIndex = 0;

        //        d_mc = new ComboBox();
        //        d_mc.DropDownStyle = ComboBoxStyle.DropDownList;

        //        d_mc.Items.AddRange(new string[] { "0", "5", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55" });
        //        d_mc.Size = new Size(40, 10);
        //        d_mc.SelectedIndex = 0;

        //        d_dc = new CheckBox();
        //        d_dc.Text = "매일";
        //        d_dc.ForeColor = Color.FromArgb(255, 255, 255, 255);
        //        d_dc.CheckedChanged += new EventHandler(dc_CheckedChanged);

        //        d_wc = new ComboBox();
        //        d_wc.DropDownStyle = ComboBoxStyle.DropDownList;
        //        d_wc.Items.AddRange(new String[] { "일", "월", "화", "수", "목", "금", "토" });
        //        d_wc.SelectedIndex = 0;
        //        d_wc.Size = new Size(40, 10);

        //        //fLayout.Controls.Add(t);
        //        d_fLayout.Controls.Add(d_hc);
        //        d_fLayout.Controls.Add(d_mc);
        //        d_fLayout.Controls.Add(d_dc);
        //        d_fLayout.Controls.Add(d_wc);
        //        d_fLayout.Controls.Add(d_wc);

        //        d_fLayout2 = new FlowLayoutPanel();
        //        h_hc = new ComboBox();
        //        h_hc.Items.AddRange(new String[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
        //        h_hc.Size = new Size(40, 10);
        //        h_hc.SelectedIndex = 0;
        //        d_fLayout2.Controls.Add(h_hc);
        //        isCreateOptionControll = true;
        //    }
        //    if (combo.SelectedIndex == 0)
        //    {
        //        if (isD_fLayout2Add)
        //        {
        //            resetOptionTable.Controls.Remove(d_fLayout2);
        //        }
        //        d_la.Text = "날짜 주기 설정";
        //        resetOptionTable.Controls.Add(d_fLayout, 1, 2);
        //        isD_fLayoutAdd = true;
        //        isD_fLayout2Add = false;
        //    }

        //    if (combo.SelectedIndex == 1)
        //    {
        //        if (isD_fLayoutAdd)
        //        {
        //            resetOptionTable.Controls.Remove(d_fLayout);
        //        }
        //        d_la.Text = "시간 주기 설정";
        //        resetOptionTable.Controls.Add(d_fLayout2, 1, 2);
        //        isD_fLayout2Add = true;
        //        isD_fLayoutAdd = false;
        //    }
        //}

        private void dc_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox dc = (CheckBox)sender;
            //if (dc.Checked)
            //{
            //    d_wc.Enabled = false;
            //}
            //else
            //{
            //    d_wc.Enabled = true;
            //}
        }

        private void rebootCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void glassButton1_Click(object sender, EventArgs e)
        {

        }

        private void resetStartBtn_Click(object sender, EventArgs e)
        {
            //this.resetOptionTable.Visible = false;
            //this.resultTextBox.Visible = true;
            //this.progressBar1.Visible = true;
        }

        #endregion





    }
}
