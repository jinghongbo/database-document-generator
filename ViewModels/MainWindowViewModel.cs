using DocumentFormat.OpenXml.Packaging;
using FreeSql;
using FreeSql.DatabaseModel;
using Prism.Mvvm;
using System.Collections.Generic;

namespace DatabaseDocumentGenerator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "DatabaseDocumentGenerator";
        private DataType _dataType;
        private string _connectionString;
        private string _documentTemplatePath;
        private IFreeSql _connection;


        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DataType DataType { get => _dataType; set => _dataType = value; }
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }

        public string DocumentTemplatePath { get => _documentTemplatePath; set => _documentTemplatePath = value; }

        public List<DbTableInfo> Tables { get; set; }

        public List<DbTableInfo> SelectedTables { get; set; }
        public MainWindowViewModel()
        {
        }

        public void Connect()
        {

            var connection = new FreeSqlBuilder().UseConnectionString(_dataType, _connectionString).Build();
            if (connection.Ado.ExecuteConnectTest())
            {
                _connection = connection;

            }
        }

        public void LoadTables()
        {
            Tables = _connection.DbFirst.GetTablesByDatabase();
        }

        public void GenerateDocument()
        {
            using var document = WordprocessingDocument.CreateFromTemplate(DocumentTemplatePath);
        }
    }
}
