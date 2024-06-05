using System.Text;

namespace CodeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button_validator_Click(object sender, EventArgs e)
        {
            var _schemas = await DatabaseHelper.ExecuteSql<string>("SELECT INITCAP(schema_name) asschema FROM information_schema.schemata where schema_name not in ('pg_catalog','information_schema') order by asschema;");

            var _tables = await DatabaseHelper.ExecuteSql<TableSchema>("SELECT schemaname, tablename FROM pg_tables where schemaname not in ('pg_catalog','information_schema') and tablename <> 'design' order by schemaname,tablename;");

            StringBuilder sb = new();

            List<string> _list = (List<string>)_schemas;
            List<TableSchema> _listTables = (List<TableSchema>)_tables;
            foreach (var item in _list)
            {
                string folderPath = @"C:\CodeGenerator";
                string newPath = folderPath + "\\" + item;
                if (!Directory.Exists(newPath))
                {
                    try
                    {
                        Directory.CreateDirectory(newPath);
                        Console.WriteLine("Folder created successfully!");

                        string _schema = item == "Inv" ? "inv" : item;
                        List<TableSchema> _listGeneratedTables = _listTables.Where(a => a.schemaname == _schema.ToLower()).ToList();
                        string _tablename;

                        foreach (var itemTable in _listGeneratedTables)
                        {
                            _tablename = UppercaseFirstCharacters(itemTable.tablename);
                            string filePath = newPath + $"\\{_tablename}Validator.cs"; // Path to your file

                            // Check if file already exists. If yes, delete it.
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }

                            // Create a new file
                            using (StreamWriter sw = File.CreateText(filePath))
                            {
                                sw.WriteLine("using FluentValidation;                                                                                           ");
                                sw.WriteLine("using Monovi.App.Cloud.Model.Entities." + item + ";                                                                     ");
                                sw.WriteLine();
                                sw.WriteLine("namespace Monovi.App.Cloud.Validation." + item + ";                                                                     ");
                                sw.WriteLine();
                                sw.WriteLine("public class " + _tablename + "Validator : AbstractValidator<ValidationEntity<" + _tablename + ">>");
                                sw.WriteLine("{                                                                                                                 ");
                                sw.WriteLine("    public " + _tablename + "Validator()                                                                  ");
                                sw.WriteLine("    {                                                                                                             ");
                                sw.WriteLine("        //RuleFor(x => x.ValidateObject.country_id)                                                               ");
                                sw.WriteLine("        //    .NotEmpty().WithMessage(x => { return $\"{ x.LocaleStringResources.Count} deneme\"; });                ");
                                sw.WriteLine("    }                                                                                                             ");
                                sw.WriteLine("}                                                                                                                 ");
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("Error creating folder: {0}", ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Folder already exists.");
                }
            }
            MessageBox.Show("Complete");
        }

        public static string UppercaseFirstCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var words = text.Split('_');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Length > 0 ? char.ToUpper(words[i][0]) + words[i].Substring(1) : "";
            }

            return string.Join("", words).Replace('Ý', 'I'); ;
        }

        private async void button1_validate_module_Click(object sender, EventArgs e)
        {
            var _schemas = await DatabaseHelper.ExecuteSql<string>("SELECT INITCAP(schema_name) asschema FROM information_schema.schemata where schema_name not in ('pg_catalog','information_schema') order by asschema;");

            var _tables = await DatabaseHelper.ExecuteSql<TableSchema>("SELECT schemaname, tablename FROM pg_tables where schemaname not in ('pg_catalog','information_schema') and tablename <> 'design' order by schemaname,tablename;");

            StringBuilder sb = new StringBuilder();

            List<string> _list = (List<string>)_schemas;
            List<TableSchema> _listTables = (List<TableSchema>)_tables;
            foreach (var item in _list)
            {
                sb.AppendLine("            #region Schema : " + item);
                sb.AppendLine();

                string _schema = item == "Inv" ? "inv" : item;

                List<TableSchema> _listGeneratedTables = _listTables.Where(a => a.schemaname == _schema.ToLower()).ToList();
                string _tablename;

                foreach (var itemTable in _listGeneratedTables)
                {
                    _tablename = UppercaseFirstCharacters(itemTable.tablename);
                    sb.AppendLine("            services.AddScoped<IValidator<ValidationEntity<" + _tablename + ">>, " + _tablename + "Validator>(); ");
                }
                sb.AppendLine();
                sb.AppendLine("            #endregion Schema : " + item);
                sb.AppendLine();
            }
            var _result = sb.ToString();
            MessageBox.Show(_result);
        }

        private async void button_autofac_Click(object sender, EventArgs e)
        {
            var _schemas = await DatabaseHelper.ExecuteSql<string>("SELECT INITCAP(schema_name) asschema FROM information_schema.schemata where schema_name not in ('pg_catalog','information_schema') order by asschema;");

            var _tables = await DatabaseHelper.ExecuteSql<TableSchema>("SELECT schemaname, tablename FROM pg_tables where schemaname not in ('pg_catalog','information_schema') and tablename <> 'design' order by schemaname,tablename;");

            StringBuilder sb = new StringBuilder();

            List<string> _list = (List<string>)_schemas;
            List<TableSchema> _listTables = (List<TableSchema>)_tables;
            foreach (var item in _list)
            {
                sb.AppendLine("            #region Schema : " + item);
                sb.AppendLine();

                string _schema = item == "Inv" ? "inv" : item;

                List<TableSchema> _listGeneratedTables = _listTables.Where(a => a.schemaname == _schema.ToLower()).ToList();
                string _tablename;

                foreach (var itemTable in _listGeneratedTables)
                {
                    _tablename = UppercaseFirstCharacters(itemTable.tablename);
                    sb.AppendLine($"            builder.RegisterType<{_tablename}Service>().As<I{_tablename}Service>().InstancePerLifetimeScope();");
                }
                sb.AppendLine();
                sb.AppendLine("            #endregion Schema : " + item);
                sb.AppendLine();
            }
            var _result = sb.ToString();
            MessageBox.Show(_result);
        }
    }
}