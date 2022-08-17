

using Excel = Microsoft.Office.Interop.Excel;


namespace ado.net
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProductDal _productDal = new ProductDal();
        //ProductDal' class�na ba�lanmak i�in nesnesini olu�turduk.

        private void LoadProducts()
        {
            dgwProducts.DataSource = _productDal.GetAll();
            //Product' lar� g�sterecek datagridwiev' e DataSource komutuyla bilgileri �ektik
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            //uygulama a��ld���nda direk veritaban� g�stermesini sa�lad�k
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name = tbxName.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text)
            });

            LoadProducts();
            MessageBox.Show("Product Added");
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            tbxNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            tbxUnitPriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            tbxStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();

        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                Name = tbxNameUpdate.Text.ToString(),
                UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text)
            };
            _productDal.Update(product);
            LoadProducts();
            MessageBox.Show("Updated!");

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value);
            _productDal.Delete(id);
            LoadProducts();
            MessageBox.Show("Deleted!");
        }

        private void rbNormal_CheckedChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void rbFirstExpensive_CheckedChanged(object sender, EventArgs e)
        {

            dgwProducts.DataSource = _productDal.GetAllFiltreExpensive();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            dgwProducts.DataSource = _productDal.GetAllFiltreCheap();

        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            dgwProducts.DataSource = _productDal.GetAllFiltreSearch(tbSearch.Text);
        }

    }
}
