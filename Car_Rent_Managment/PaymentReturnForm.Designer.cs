using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class PaymentReturnForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblCustomer;
        private Button btnRefresh;

        private Panel paymentBox;
        private Label lblPaymentTitle;
        private Label lblPaymentSub;
        private FlowLayoutPanel flpPayments;

        private Panel paymentDetailsPanel;
        private Label lblPaymentDetailsTitle;
        private Label lblSelectedPayment;
        private Label lblPaymentCar;
        private Label lblPaymentAmount;
        private Label lblMethodTitle;
        private FlowLayoutPanel flpPaymentMethods;
        private Label lblSelectedMethod;
        private Label lblTransactionNumber;
        private TextBox txtTransactionNumber;
        private Button btnPay;

        private Panel returnBox;
        private Label lblReturnTitle;
        private Label lblReturnSub;
        private FlowLayoutPanel flpReturns;

        private Panel returnDetailsPanel;
        private Label lblReturnDetailsTitle;
        private Label lblSelectedReturn;
        private Label lblReturnCar;
        private Label lblExpectedReturn;
        private Label lblActualReturn;
        private DateTimePicker dtpActualReturnDate;
        private Label lblLateDays;
        private Label lblFinePreview;
        private Button btnReturn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblCustomer = new Label();
            btnRefresh = new Button();
            paymentBox = new Panel();
            lblPaymentTitle = new Label();
            lblPaymentSub = new Label();
            flpPayments = new FlowLayoutPanel();
            paymentDetailsPanel = new Panel();
            lblPaymentDetailsTitle = new Label();
            lblSelectedPayment = new Label();
            lblPaymentCar = new Label();
            lblPaymentAmount = new Label();
            lblMethodTitle = new Label();
            flpPaymentMethods = new FlowLayoutPanel();
            lblSelectedMethod = new Label();
            lblTransactionNumber = new Label();
            txtTransactionNumber = new TextBox();
            btnPay = new Button();
            returnBox = new Panel();
            lblReturnTitle = new Label();
            lblReturnSub = new Label();
            flpReturns = new FlowLayoutPanel();
            returnDetailsPanel = new Panel();
            lblReturnDetailsTitle = new Label();
            lblSelectedReturn = new Label();
            lblReturnCar = new Label();
            lblExpectedReturn = new Label();
            lblActualReturn = new Label();
            dtpActualReturnDate = new DateTimePicker();
            lblLateDays = new Label();
            lblFinePreview = new Label();
            btnReturn = new Button();
            paymentBox.SuspendLayout();
            paymentDetailsPanel.SuspendLayout();
            returnBox.SuspendLayout();
            returnDetailsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(35, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(268, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Payment & Return";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(40, 72);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(347, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Complete payment first, then return paid active rentals.";
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCustomer.ForeColor = Color.FromArgb(15, 23, 42);
            lblCustomer.Location = new Point(40, 98);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(78, 19);
            lblCustomer.TabIndex = 2;
            lblCustomer.Text = "Customer:";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(1160, 45);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 40);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // paymentBox
            // 
            paymentBox.BackColor = Color.White;
            paymentBox.Controls.Add(lblPaymentTitle);
            paymentBox.Controls.Add(lblPaymentSub);
            paymentBox.Controls.Add(flpPayments);
            paymentBox.Controls.Add(paymentDetailsPanel);
            paymentBox.Location = new Point(30, 120);
            paymentBox.Name = "paymentBox";
            paymentBox.Size = new Size(1278, 335);
            paymentBox.TabIndex = 4;
            // 
            // lblPaymentTitle
            // 
            lblPaymentTitle.AutoSize = true;
            lblPaymentTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblPaymentTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblPaymentTitle.Location = new Point(20, 15);
            lblPaymentTitle.Name = "lblPaymentTitle";
            lblPaymentTitle.Size = new Size(118, 28);
            lblPaymentTitle.TabIndex = 0;
            lblPaymentTitle.Text = "1. Payment";
            // 
            // lblPaymentSub
            // 
            lblPaymentSub.AutoSize = true;
            lblPaymentSub.Font = new Font("Segoe UI", 9F);
            lblPaymentSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblPaymentSub.Location = new Point(23, 48);
            lblPaymentSub.Name = "lblPaymentSub";
            lblPaymentSub.Size = new Size(165, 15);
            lblPaymentSub.TabIndex = 1;
            lblPaymentSub.Text = "Unpaid bookings appear here.";
            // 
            // flpPayments
            // 
            flpPayments.AutoScroll = true;
            flpPayments.BackColor = Color.FromArgb(248, 250, 252);
            flpPayments.Location = new Point(20, 70);
            flpPayments.Name = "flpPayments";
            flpPayments.Size = new Size(760, 251);
            flpPayments.TabIndex = 2;
            // 
            // paymentDetailsPanel
            // 
            paymentDetailsPanel.Controls.Add(lblPaymentDetailsTitle);
            paymentDetailsPanel.Controls.Add(lblSelectedPayment);
            paymentDetailsPanel.Controls.Add(lblPaymentCar);
            paymentDetailsPanel.Controls.Add(lblPaymentAmount);
            paymentDetailsPanel.Controls.Add(lblMethodTitle);
            paymentDetailsPanel.Controls.Add(flpPaymentMethods);
            paymentDetailsPanel.Controls.Add(lblSelectedMethod);
            paymentDetailsPanel.Controls.Add(lblTransactionNumber);
            paymentDetailsPanel.Controls.Add(txtTransactionNumber);
            paymentDetailsPanel.Controls.Add(btnPay);
            paymentDetailsPanel.Location = new Point(805, 50);
            paymentDetailsPanel.Name = "paymentDetailsPanel";
            paymentDetailsPanel.Size = new Size(470, 282);
            paymentDetailsPanel.TabIndex = 3;
            paymentDetailsPanel.Paint += paymentDetailsPanel_Paint;
            // 
            // lblPaymentDetailsTitle
            // 
            lblPaymentDetailsTitle.AutoSize = true;
            lblPaymentDetailsTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPaymentDetailsTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblPaymentDetailsTitle.Location = new Point(18, 15);
            lblPaymentDetailsTitle.Name = "lblPaymentDetailsTitle";
            lblPaymentDetailsTitle.Size = new Size(150, 25);
            lblPaymentDetailsTitle.TabIndex = 0;
            lblPaymentDetailsTitle.Text = "Payment Details";
            // 
            // lblSelectedPayment
            // 
            lblSelectedPayment.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectedPayment.Location = new Point(18, 50);
            lblSelectedPayment.Name = "lblSelectedPayment";
            lblSelectedPayment.Size = new Size(150, 25);
            lblSelectedPayment.TabIndex = 1;
            lblSelectedPayment.Text = "Selected Booking: None";
            // 
            // lblPaymentCar
            // 
            lblPaymentCar.Font = new Font("Segoe UI", 10F);
            lblPaymentCar.Location = new Point(18, 89);
            lblPaymentCar.Name = "lblPaymentCar";
            lblPaymentCar.Size = new Size(150, 25);
            lblPaymentCar.TabIndex = 2;
            lblPaymentCar.Text = "Car: -";
            // 
            // lblPaymentAmount
            // 
            lblPaymentAmount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPaymentAmount.ForeColor = Color.FromArgb(37, 99, 235);
            lblPaymentAmount.Location = new Point(18, 125);
            lblPaymentAmount.Name = "lblPaymentAmount";
            lblPaymentAmount.Size = new Size(150, 23);
            lblPaymentAmount.TabIndex = 3;
            lblPaymentAmount.Text = "Amount: 0 BDT";
            // 
            // lblMethodTitle
            // 
            lblMethodTitle.AutoSize = true;
            lblMethodTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMethodTitle.Location = new Point(205, 19);
            lblMethodTitle.Name = "lblMethodTitle";
            lblMethodTitle.Size = new Size(61, 19);
            lblMethodTitle.TabIndex = 4;
            lblMethodTitle.Text = "Method";
            // 
            // flpPaymentMethods
            // 
            flpPaymentMethods.BackColor = Color.FromArgb(248, 250, 252);
            flpPaymentMethods.Location = new Point(205, 50);
            flpPaymentMethods.Name = "flpPaymentMethods";
            flpPaymentMethods.Size = new Size(240, 188);
            flpPaymentMethods.TabIndex = 5;
            // 
            // lblSelectedMethod
            // 
            lblSelectedMethod.Font = new Font("Segoe UI", 10F);
            lblSelectedMethod.Location = new Point(18, 161);
            lblSelectedMethod.Name = "lblSelectedMethod";
            lblSelectedMethod.Size = new Size(145, 22);
            lblSelectedMethod.TabIndex = 6;
            lblSelectedMethod.Text = "Method: bKash";
            lblSelectedMethod.Click += lblSelectedMethod_Click;
            // 
            // lblTransactionNumber
            // 
            lblTransactionNumber.AutoSize = true;
            lblTransactionNumber.Font = new Font("Segoe UI", 10F);
            lblTransactionNumber.Location = new Point(18, 183);
            lblTransactionNumber.Name = "lblTransactionNumber";
            lblTransactionNumber.Size = new Size(96, 19);
            lblTransactionNumber.TabIndex = 7;
            lblTransactionNumber.Text = "Transaction ID";
            // 
            // txtTransactionNumber
            // 
            txtTransactionNumber.BackColor = Color.FromArgb(241, 245, 249);
            txtTransactionNumber.Font = new Font("Segoe UI", 9F);
            txtTransactionNumber.Location = new Point(18, 205);
            txtTransactionNumber.Name = "txtTransactionNumber";
            txtTransactionNumber.ReadOnly = true;
            txtTransactionNumber.Size = new Size(145, 23);
            txtTransactionNumber.TabIndex = 8;
            // 
            // btnPay
            // 
            btnPay.Enabled = false;
            btnPay.Location = new Point(18, 234);
            btnPay.Name = "btnPay";
            btnPay.Size = new Size(145, 34);
            btnPay.TabIndex = 9;
            btnPay.Text = "Confirm Payment";
            btnPay.Click += btnPay_Click;
            // 
            // returnBox
            // 
            returnBox.BackColor = Color.White;
            returnBox.Controls.Add(lblReturnTitle);
            returnBox.Controls.Add(lblReturnSub);
            returnBox.Controls.Add(flpReturns);
            returnBox.Controls.Add(returnDetailsPanel);
            returnBox.Location = new Point(30, 461);
            returnBox.Name = "returnBox";
            returnBox.Size = new Size(1278, 309);
            returnBox.TabIndex = 5;
            // 
            // lblReturnTitle
            // 
            lblReturnTitle.AutoSize = true;
            lblReturnTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblReturnTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblReturnTitle.Location = new Point(20, 15);
            lblReturnTitle.Name = "lblReturnTitle";
            lblReturnTitle.Size = new Size(136, 28);
            lblReturnTitle.TabIndex = 0;
            lblReturnTitle.Text = "2. Return Car";
            // 
            // lblReturnSub
            // 
            lblReturnSub.AutoSize = true;
            lblReturnSub.Font = new Font("Segoe UI", 9F);
            lblReturnSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblReturnSub.Location = new Point(23, 48);
            lblReturnSub.Name = "lblReturnSub";
            lblReturnSub.Size = new Size(233, 15);
            lblReturnSub.TabIndex = 1;
            lblReturnSub.Text = "Only paid active bookings can be returned.";
            // 
            // flpReturns
            // 
            flpReturns.AutoScroll = true;
            flpReturns.BackColor = Color.FromArgb(248, 250, 252);
            flpReturns.Location = new Point(20, 70);
            flpReturns.Name = "flpReturns";
            flpReturns.Size = new Size(810, 218);
            flpReturns.TabIndex = 2;
            // 
            // returnDetailsPanel
            // 
            returnDetailsPanel.Controls.Add(lblReturnDetailsTitle);
            returnDetailsPanel.Controls.Add(lblSelectedReturn);
            returnDetailsPanel.Controls.Add(lblReturnCar);
            returnDetailsPanel.Controls.Add(lblExpectedReturn);
            returnDetailsPanel.Controls.Add(lblActualReturn);
            returnDetailsPanel.Controls.Add(dtpActualReturnDate);
            returnDetailsPanel.Controls.Add(lblLateDays);
            returnDetailsPanel.Controls.Add(lblFinePreview);
            returnDetailsPanel.Controls.Add(btnReturn);
            returnDetailsPanel.Location = new Point(860, 50);
            returnDetailsPanel.Name = "returnDetailsPanel";
            returnDetailsPanel.Size = new Size(390, 238);
            returnDetailsPanel.TabIndex = 3;
            // 
            // lblReturnDetailsTitle
            // 
            lblReturnDetailsTitle.AutoSize = true;
            lblReturnDetailsTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblReturnDetailsTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblReturnDetailsTitle.Location = new Point(18, 15);
            lblReturnDetailsTitle.Name = "lblReturnDetailsTitle";
            lblReturnDetailsTitle.Size = new Size(133, 25);
            lblReturnDetailsTitle.TabIndex = 0;
            lblReturnDetailsTitle.Text = "Return Details";
            // 
            // lblSelectedReturn
            // 
            lblSelectedReturn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectedReturn.Location = new Point(18, 50);
            lblSelectedReturn.Name = "lblSelectedReturn";
            lblSelectedReturn.Size = new Size(220, 25);
            lblSelectedReturn.TabIndex = 1;
            lblSelectedReturn.Text = "Selected Booking: None";
            // 
            // lblReturnCar
            // 
            lblReturnCar.Font = new Font("Segoe UI", 10F);
            lblReturnCar.Location = new Point(18, 78);
            lblReturnCar.Name = "lblReturnCar";
            lblReturnCar.Size = new Size(220, 25);
            lblReturnCar.TabIndex = 2;
            lblReturnCar.Text = "Car: -";
            // 
            // lblExpectedReturn
            // 
            lblExpectedReturn.Font = new Font("Segoe UI", 10F);
            lblExpectedReturn.Location = new Point(18, 105);
            lblExpectedReturn.Name = "lblExpectedReturn";
            lblExpectedReturn.Size = new Size(220, 25);
            lblExpectedReturn.TabIndex = 3;
            lblExpectedReturn.Text = "Expected Return: -";
            // 
            // lblActualReturn
            // 
            lblActualReturn.AutoSize = true;
            lblActualReturn.Font = new Font("Segoe UI", 10F);
            lblActualReturn.Location = new Point(240, 50);
            lblActualReturn.Name = "lblActualReturn";
            lblActualReturn.Size = new Size(92, 19);
            lblActualReturn.TabIndex = 4;
            lblActualReturn.Text = "Actual Return";
            // 
            // dtpActualReturnDate
            // 
            dtpActualReturnDate.Font = new Font("Segoe UI", 10F);
            dtpActualReturnDate.Format = DateTimePickerFormat.Short;
            dtpActualReturnDate.Location = new Point(240, 78);
            dtpActualReturnDate.Name = "dtpActualReturnDate";
            dtpActualReturnDate.Size = new Size(110, 25);
            dtpActualReturnDate.TabIndex = 5;
            dtpActualReturnDate.ValueChanged += dtpActualReturnDate_ValueChanged;
            // 
            // lblLateDays
            // 
            lblLateDays.AutoSize = true;
            lblLateDays.Font = new Font("Segoe UI", 10F);
            lblLateDays.Location = new Point(240, 111);
            lblLateDays.Name = "lblLateDays";
            lblLateDays.Size = new Size(84, 19);
            lblLateDays.TabIndex = 6;
            lblLateDays.Text = "Late Days: 0";
            // 
            // lblFinePreview
            // 
            lblFinePreview.AutoSize = true;
            lblFinePreview.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblFinePreview.ForeColor = Color.FromArgb(217, 119, 6);
            lblFinePreview.Location = new Point(104, 153);
            lblFinePreview.Name = "lblFinePreview";
            lblFinePreview.Size = new Size(147, 20);
            lblFinePreview.TabIndex = 7;
            lblFinePreview.Text = "Fine Preview: 0 BDT";
            // 
            // btnReturn
            // 
            btnReturn.Enabled = false;
            btnReturn.Location = new Point(18, 186);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(332, 36);
            btnReturn.TabIndex = 8;
            btnReturn.Text = "Confirm Return";
            btnReturn.Click += btnReturn_Click;
            // 
            // PaymentReturnForm
            // 
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1320, 811);
            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblCustomer);
            Controls.Add(btnRefresh);
            Controls.Add(paymentBox);
            Controls.Add(returnBox);
            Name = "PaymentReturnForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Payment & Return";
            paymentBox.ResumeLayout(false);
            paymentBox.PerformLayout();
            paymentDetailsPanel.ResumeLayout(false);
            paymentDetailsPanel.PerformLayout();
            returnBox.ResumeLayout(false);
            returnBox.PerformLayout();
            returnDetailsPanel.ResumeLayout(false);
            returnDetailsPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
