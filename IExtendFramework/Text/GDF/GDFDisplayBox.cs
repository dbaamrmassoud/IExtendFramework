namespace IExtendFramework.Text.GDF
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// Displays GDF Output
    /// </summary>
    public partial class GDFDisplayBox : UserControl
    {
        #region Fields

        private readonly BindingSource bindingsource;

        #endregion

        #region Constructor

        public GDFDisplayBox()
        {
            this.InitializeComponent();
            this.bindingsource = new BindingSource(this.components);
            this.bindingNavigator1.BindingSource = this.bindingsource;
            this.bindingsource.CurrentItemChanged += this.BindingSource_CurrentItemChanged;
            this.bindingsource.DataSourceChanged += this.bindingsource_DataSourceChanged;
        }

        #endregion

        #region Event Handlers

        private void BindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            GDFPage page = this.bindingNavigator1.BindingSource.Current as GDFPage;
            if (page != null)
            {
                this.pictureBox1.Size = page.PageImage.Size;
                this.pictureBox1.Image = page.PageImage;
            }
        }

        private void bindingsource_DataSourceChanged(object sender, EventArgs e)
        {
            GDFPage page = this.bindingNavigator1.BindingSource.Current as GDFPage;
            if (page != null)
            {
                this.pictureBox1.Size = page.PageImage.Size;
                this.pictureBox1.Image = page.PageImage;
            }
        }

        private void GDFDisplayBox_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Public Methods

        public void SetPages(List <GDFPage> pages)
        {
            this.bindingNavigator1.BindingSource.DataSource = pages;
        }

        #endregion
    }
}