using Phuoc_C3_B1.UserControls;
using Phuoc_C3_B1.UserControls.ViewInvoiceDetailsBy;
using Phuoc_C3_B1.UserControls.ViewReceiptBy;
using System.Windows;
using System.Windows.Controls;


namespace Phuoc_C3_B1.Windows
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            btn_logOut.Click += Btn_logOut_Click;

            lb_Create.SelectionChanged += Lb_Create_SelectionChanged;
            lb_Update.SelectionChanged += Lb_Update_SelectionChanged;
            lb_View.SelectionChanged += Lb_View_SelectionChanged;
        }


        private void Btn_logOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }


        private void Lb_Create_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Create.SelectedItem == null) return;

            sp_userControlContent.Children.Clear();
            lb_Update.SelectedItem = null;
            lb_View.SelectedItem = null;

            ListBoxItem item = lb_Create.SelectedItem as ListBoxItem;

            switch (item.Name)
            {
                case "create_receipt":
                    uc_CreateReceipt createReceipt = new uc_CreateReceipt();
                    sp_userControlContent.Children.Add(createReceipt);
                    break;

                case "create_invoice":
                    uc_CreateInvoice createInvoice = new uc_CreateInvoice();
                    sp_userControlContent.Children.Add(createInvoice);
                    break;
            }
        }


        private void Lb_Update_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Update.SelectedItem == null) return;

            sp_userControlContent.Children.Clear();
            lb_Create.SelectedItem = null;
            lb_View.SelectedItem = null;

            ListBoxItem item = lb_Update.SelectedItem as ListBoxItem;

            switch (item.Name)
            {
                case "update_productPrice":
                    uc_UpdateProductPriceInput updateProductPriceInput = new uc_UpdateProductPriceInput();
                    sp_userControlContent.Children.Add(updateProductPriceInput);
                    break;
            }
        }


        private void Lb_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_View.SelectedItem == null) return;

            sp_userControlContent.Children.Clear();
            lb_Create.SelectedItem = null;
            lb_Update.SelectedItem = null;

            ListBoxItem item = lb_View.SelectedItem as ListBoxItem;

            switch (item.Name)
            {
                case "view_products":
                    uc_ViewProducts viewProducts = new uc_ViewProducts();
                    sp_userControlContent.Children.Add(viewProducts);
                    break;
                case "view_stocks":
                    uc_ViewStocks viewStock = new uc_ViewStocks();
                    sp_userControlContent.Children.Add(viewStock);
                    break;
                case "view_receipts":
                    uc_ViewReceipts viewReceipt = new uc_ViewReceipts();
                    sp_userControlContent.Children.Add(viewReceipt);
                    break;
                case "view_receiptsByDate":
                    uc_ViewReceiptByDate receiptByDate = new uc_ViewReceiptByDate();
                    sp_userControlContent.Children.Add(receiptByDate);
                    break;
                case "view_receiptDetailsByReceiptId":
                    uc_ViewReceiptDetailsByReceiptId receiptDetailsByReceiptId = new uc_ViewReceiptDetailsByReceiptId();
                    sp_userControlContent.Children.Add(receiptDetailsByReceiptId);
                    break;
                case "view_invoices":
                    uc_ViewInvoices invoices = new uc_ViewInvoices();
                    sp_userControlContent.Children.Add(invoices);
                    break;
                case "view_invoiceDetailsByDate":
                    uc_ViewInvoiceDetailsByDate invoiceDetailsByDate = new uc_ViewInvoiceDetailsByDate();
                    sp_userControlContent.Children.Add(invoiceDetailsByDate);
                    break;
                case "view_invoiceDetailsByInvoiceId":
                    uc_ViewInvoiceDetailsByInvoiceId invoiceDetailsByInvoiceId = new uc_ViewInvoiceDetailsByInvoiceId();
                    sp_userControlContent.Children.Add(invoiceDetailsByInvoiceId);
                    break;
                case "view_expiredFood":
                    uc_ViewExpiredFood expiredFood = new uc_ViewExpiredFood();
                    sp_userControlContent.Children.Add(expiredFood);
                    break;
            }
        }
    }
}
