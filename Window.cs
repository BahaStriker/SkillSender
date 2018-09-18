// Decompiled with JetBrains decompiler
// Type: Enhance.Window
// Assembly: Enhance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 19C01E0B-DD59-4615-AF59-E27D0274D556
// Assembly location: C:\Users\Maroon\Desktop\VOID SKILL SENDER.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Enhance
{
  public class Window : Form, IDisposable
  {
    private IContainer components = (IContainer) null;
    private int step = 0;
    private EventHandler<TriggerEventArgs> SettingsUpdated;
    private EventHandler Closed;
    private GroupBox ClientGB;
    private Button ConnectBTN;
    private Label StatusLB;
    private Label ClassLB;
    private Label NameLB;
    private GroupBox SettingsGB;
    private CheckBox ChargeCB;
    private CheckBox BuffCB;
    private CheckBox SelfCB;
    private CheckBox AttackCB;
    private TextBox DelayTB;
    private Label VersLB;
    private NetworkStream stream;
    private PictureBox pictureBox1;

    public Window()
    {
      this.InitializeComponent();
    }

    private void ConnectBTN_Click(object sender, EventArgs e)
    {
      try
      {
        Manager manager = (Manager) null;
        for (int index = 0; index < Process.GetProcessesByName("elementclient").Length; ++index)
        {
          manager = new Manager(index);
          if (manager.Connected)
          {
            this.SetupManager(manager);
            manager.Connect();
            this.ConnectBTN.Visible = false;
            this.NameLB.Visible = true;
            break;
          }
        }
        if (manager != null)
          ;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
      }
    }

    private void SetupManager(Manager manager)
    {
      manager.OnConnect += new EventHandler<ClientEventArgs>(this.OnConnect);
      this.SettingsUpdated += manager.OnSettingsChanged;
      this.Closed += new EventHandler(manager.OnClose);
    }

    private void OnConnect(object sender, ClientEventArgs e)
    {
      this.NameLB.Text = e.Name;
      this.ClassLB.Text = e.Class;
      this.StatusLB.Text = "Connected";
      this.StatusLB.ForeColor = Color.Chartreuse;
    }

    private void OnChangeStatus(object sender, EventArgs e)
    {
      if (this.StatusLB.ForeColor == Color.Red)
      {
        this.StatusLB.Text = "Connected";
        this.StatusLB.ForeColor = Color.Chartreuse;
      }
      else
      {
        this.StatusLB.Text = "Disconnected.";
        this.StatusLB.ForeColor = Color.Red;
      }
    }

    private void AttackCB_CheckedChanged(object sender, EventArgs e)
    {
      this.SettingsUpdated(sender, new TriggerEventArgs(16 | (((CheckBox) sender).Checked ? 1 : 0)));
    }

    private void SelfCB_CheckedChanged(object sender, EventArgs e)
    {
      this.SettingsUpdated(sender, new TriggerEventArgs(32 | (((CheckBox) sender).Checked ? 1 : 0)));
    }

    private void BuffCB_CheckedChanged(object sender, EventArgs e)
    {
      this.SettingsUpdated(sender, new TriggerEventArgs(48 | (((CheckBox) sender).Checked ? 1 : 0)));
    }

    private void ChargeCB_CheckedChanged(object sender, EventArgs e)
    {
      this.SettingsUpdated(sender, new TriggerEventArgs(64 | (((CheckBox) sender).Checked ? 1 : 0)));
    }

    public new void Dispose()
    {
      if (this.Closed == null)
        return;
      this.Closed((object) this, new EventArgs());
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Window));
      this.ClientGB = new GroupBox();
      this.ConnectBTN = new Button();
      this.StatusLB = new Label();
      this.ClassLB = new Label();
      this.NameLB = new Label();
      this.SettingsGB = new GroupBox();
      this.ChargeCB = new CheckBox();
      this.BuffCB = new CheckBox();
      this.SelfCB = new CheckBox();
      this.AttackCB = new CheckBox();
      this.DelayTB = new TextBox();
      this.VersLB = new Label();
      this.pictureBox1 = new PictureBox();
      this.ClientGB.SuspendLayout();
      this.SettingsGB.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.ClientGB.BackColor = Color.White;
      this.ClientGB.Controls.Add((Control) this.ConnectBTN);
      this.ClientGB.Controls.Add((Control) this.StatusLB);
      this.ClientGB.Controls.Add((Control) this.ClassLB);
      this.ClientGB.Controls.Add((Control) this.NameLB);
      this.ClientGB.ForeColor = SystemColors.ActiveCaptionText;
      this.ClientGB.Location = new Point(23, 91);
      this.ClientGB.Name = "ClientGB";
      this.ClientGB.Size = new Size(166, 76);
      this.ClientGB.TabIndex = 0;
      this.ClientGB.TabStop = false;
      this.ClientGB.Text = "Client";
      this.ConnectBTN.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.ConnectBTN.ForeColor = SystemColors.ActiveCaptionText;
      this.ConnectBTN.Location = new Point(26, 22);
      this.ConnectBTN.Name = "ConnectBTN";
      this.ConnectBTN.Size = new Size(113, 35);
      this.ConnectBTN.TabIndex = 3;
      this.ConnectBTN.Text = "CONNECT";
      this.ConnectBTN.UseVisualStyleBackColor = true;
      this.ConnectBTN.Click += new EventHandler(this.ConnectBTN_Click);
      this.StatusLB.AutoSize = true;
      this.StatusLB.Location = new Point(7, 50);
      this.StatusLB.Name = "StatusLB";
      this.StatusLB.Size = new Size(0, 13);
      this.StatusLB.TabIndex = 2;
      this.StatusLB.Visible = false;
      this.ClassLB.AutoSize = true;
      this.ClassLB.Location = new Point(7, 34);
      this.ClassLB.Name = "ClassLB";
      this.ClassLB.Size = new Size(0, 13);
      this.ClassLB.TabIndex = 1;
      this.ClassLB.Visible = false;
      this.NameLB.AutoSize = true;
      this.NameLB.Location = new Point(6, 18);
      this.NameLB.Name = "NameLB";
      this.NameLB.Size = new Size(0, 13);
      this.NameLB.TabIndex = 0;
      this.NameLB.Visible = false;
      this.SettingsGB.Controls.Add((Control) this.ChargeCB);
      this.SettingsGB.Controls.Add((Control) this.BuffCB);
      this.SettingsGB.Controls.Add((Control) this.SelfCB);
      this.SettingsGB.Controls.Add((Control) this.AttackCB);
      this.SettingsGB.ForeColor = SystemColors.ActiveCaptionText;
      this.SettingsGB.Location = new Point(23, 173);
      this.SettingsGB.Name = "SettingsGB";
      this.SettingsGB.Size = new Size(166, 118);
      this.SettingsGB.TabIndex = 1;
      this.SettingsGB.TabStop = false;
      this.SettingsGB.Text = "Settings";
      this.SettingsGB.Enter += new EventHandler(this.SettingsGB_Enter);
      this.ChargeCB.AutoSize = true;
      this.ChargeCB.Checked = true;
      this.ChargeCB.CheckState = CheckState.Checked;
      this.ChargeCB.Location = new Point(12, 91);
      this.ChargeCB.Name = "ChargeCB";
      this.ChargeCB.Size = new Size(151, 17);
      this.ChargeCB.TabIndex = 3;
      this.ChargeCB.Text = "Auto-complete charging";
      this.ChargeCB.UseVisualStyleBackColor = true;
      this.ChargeCB.CheckedChanged += new EventHandler(this.ChargeCB_CheckedChanged);
      this.BuffCB.AutoSize = true;
      this.BuffCB.Checked = true;
      this.BuffCB.CheckState = CheckState.Checked;
      this.BuffCB.Location = new Point(12, 68);
      this.BuffCB.Name = "BuffCB";
      this.BuffCB.Size = new Size(123, 17);
      this.BuffCB.TabIndex = 2;
      this.BuffCB.Text = "Enhance buff skills";
      this.BuffCB.UseVisualStyleBackColor = true;
      this.BuffCB.CheckedChanged += new EventHandler(this.BuffCB_CheckedChanged);
      this.SelfCB.AutoSize = true;
      this.SelfCB.Checked = true;
      this.SelfCB.CheckState = CheckState.Checked;
      this.SelfCB.Location = new Point(12, 45);
      this.SelfCB.Name = "SelfCB";
      this.SelfCB.Size = new Size(119, 17);
      this.SelfCB.TabIndex = 1;
      this.SelfCB.Text = "Enhance self skills";
      this.SelfCB.UseVisualStyleBackColor = true;
      this.SelfCB.CheckedChanged += new EventHandler(this.SelfCB_CheckedChanged);
      this.AttackCB.AutoSize = true;
      this.AttackCB.Checked = true;
      this.AttackCB.CheckState = CheckState.Checked;
      this.AttackCB.Location = new Point(12, 23);
      this.AttackCB.Name = "AttackCB";
      this.AttackCB.Size = new Size(132, 17);
      this.AttackCB.TabIndex = 0;
      this.AttackCB.Text = "Enhance attack skills";
      this.AttackCB.UseVisualStyleBackColor = true;
      this.AttackCB.CheckedChanged += new EventHandler(this.AttackCB_CheckedChanged);
      this.DelayTB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.DelayTB.Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.DelayTB.Location = new Point(142, 297);
      this.DelayTB.Name = "DelayTB";
      this.DelayTB.Size = new Size(44, 23);
      this.DelayTB.TabIndex = 3;
      this.DelayTB.Text = "N/A";
      this.DelayTB.TextAlign = HorizontalAlignment.Center;
      this.VersLB.AutoSize = true;
      this.VersLB.BackColor = Color.White;
      this.VersLB.ForeColor = SystemColors.ActiveCaptionText;
      this.VersLB.Location = new Point(20, 310);
      this.VersLB.Name = "VersLB";
      this.VersLB.Size = new Size(27, 13);
      this.VersLB.TabIndex = 4;
      this.VersLB.Text = "v1.1";
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(23, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(166, 64);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 5;
      this.pictureBox1.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(216, 332);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.VersLB);
      this.Controls.Add((Control) this.DelayTB);
      this.Controls.Add((Control) this.SettingsGB);
      this.Controls.Add((Control) this.ClientGB);
      this.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.ForeColor = SystemColors.ButtonHighlight;
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Window);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Void";
      this.FormClosing += new FormClosingEventHandler(this.Window_FormClosing);
      this.Load += new EventHandler(this.Window_Load);
      this.ClientGB.ResumeLayout(false);
      this.ClientGB.PerformLayout();
      this.SettingsGB.ResumeLayout(false);
      this.SettingsGB.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public string checkMD5(string filename)
    {
      FileStream fileStream = new FileStream(filename, FileMode.Open);
      byte[] hash = new MD5CryptoServiceProvider().ComputeHash((Stream) fileStream);
      fileStream.Close();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash.Length; ++index)
        stringBuilder.Append(hash[index].ToString("x2"));
      return stringBuilder.ToString();
    }

    private void Window_FormClosing(object sender, FormClosingEventArgs e)
    {
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(Window.ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags, StringBuilder lpExeName, out int size);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hHandle);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);

    private static Process GetProcessByHandle(IntPtr hwnd)
    {
      try
      {
        uint processId;
        Window.GetWindowThreadProcessId(hwnd, out processId);
        return Process.GetProcessById((int) processId);
      }
      catch
      {
        return (Process) null;
      }
    }

    private static string GetExecutablePathAboveVista(int ProcessId)
    {
      StringBuilder lpExeName = new StringBuilder(1024);
      IntPtr num = Window.OpenProcess(Window.ProcessAccessFlags.PROCESS_QUERY_LIMITED_INFORMATION, false, ProcessId);
      if (num != IntPtr.Zero)
      {
        try
        {
          int size = lpExeName.Capacity;
          if (Window.QueryFullProcessImageName(num, 0, lpExeName, out size))
            return lpExeName.ToString();
        }
        finally
        {
          Window.CloseHandle(num);
        }
      }
      return (string) null;
    }

    private static string GetWindowPath(IntPtr hwind)
    {
      try
      {
        Process processByHandle = Window.GetProcessByHandle(hwind);
        if (Environment.OSVersion.Version.Major >= 6)
        {
          string executablePathAboveVista = Window.GetExecutablePathAboveVista(processByHandle.Id);
          if (!string.IsNullOrWhiteSpace(executablePathAboveVista))
            return executablePathAboveVista;
        }
        if (processByHandle != null)
          return processByHandle.MainModule.FileName;
        return (string) null;
      }
      catch
      {
        return (string) null;
      }
    }

    private void Window_Load(object sender, EventArgs e)
    {
      try
      {
       
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Could not find window.");
        Application.Exit();
      }
    }

    private void SettingsGB_Enter(object sender, EventArgs e)
    {
    }

    [Flags]
    private enum ProcessAccessFlags : uint
    {
      All = 2035711, // 0x001F0FFF
      Terminate = 1,
      CreateThread = 2,
      VMOperation = 8,
      VMRead = 16, // 0x00000010
      VMWrite = 32, // 0x00000020
      DupHandle = 64, // 0x00000040
      SetInformation = 512, // 0x00000200
      QueryInformation = 1024, // 0x00000400
      Synchronize = 1048576, // 0x00100000
      ReadControl = 131072, // 0x00020000
      PROCESS_QUERY_LIMITED_INFORMATION = 4096, // 0x00001000
    }
  }
}
