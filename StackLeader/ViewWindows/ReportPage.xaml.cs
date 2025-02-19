using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using OfficeOpenXml;

namespace StackLeader.ViewWindows
{
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
            LoadEmployees();
        }
        public void LoadEmployees()
        {
            EmployeeData employeeData = new EmployeeData();
            List<EmployeeData> employees = employeeData.employeeListData();
            AssignedToComboBox.ItemsSource = employees;
            AssignedToComboBox.DisplayMemberPath = "FullName";
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранные пользователем даты
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;

            if (startDate.HasValue && endDate.HasValue && startDate <= endDate)
            {
                // Загружаем отчет о выполненных задачах за выбранный период времени
                LoadCompletedTasksReport(startDate.Value, endDate.Value);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите корректный период времени для отчета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UploadReport_Click(Object sender, RoutedEventArgs e)
        {
            SaveReportToExcel();
        }
        private void SaveReportToExcel()
        {
            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("CompletedTasks");

                    // Заголовки столбцов
                    for (int i = 0; i < TasksDataGrid.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = TasksDataGrid.Columns[i].Header;
                    }

                    // Данные
                    for (int i = 0; i < TasksDataGrid.Items.Count; i++)
                    {
                        for (int j = 0; j < TasksDataGrid.Columns.Count; j++)
                        {
                            var cellValue = ((TextBlock)TasksDataGrid.Columns[j].GetCellContent(TasksDataGrid.Items[i])).Text;
                            worksheet.Cells[i + 2, j + 1].Value = cellValue;
                        }
                    }

                    // Сохраняем файл Excel
                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                    {
                        Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                        DefaultExt = ".xlsx"
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        FileInfo fi = new FileInfo(saveFileDialog.FileName);
                        excelPackage.SaveAs(fi);
                        MessageBox.Show("Отчет успешно сохранен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadCompletedTasksReport(DateTime startDate, DateTime endDate)
        {
            TaskPeriodData tpd = new TaskPeriodData();
            List<TaskPeriodData> TaskPeriodData = tpd.GetCompletedTasksForPeriod(startDate, endDate);
            TasksDataGrid.ItemsSource = TaskPeriodData;
        }
    }
}

