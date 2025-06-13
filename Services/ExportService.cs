using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using StudentManagementApp.Models;

namespace StudentManagementApp.Services
{
    public interface IExportService
    {
        byte[] ExportToExcel(List<Student> students);
        byte[] ExportToPdf(List<Student> students);
    }

    public class ExportSerive : IExportService
    {
        public byte[] ExportToPdf(List<Student> students)
        {
            using var stream = new MemoryStream();
            var document = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            PdfWriter.GetInstance(document, stream);

            document.Open();

            var title = new Paragraph(
                "Students",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)
            );
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20;
            document.Add(title);

            var table = new PdfPTable(6);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 0.5f, 1, 2, 2, 2, 1 });

            var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            var headers = new[]
            {
                "",
                "Student Number",
                "First Name",
                "Last Name",
                "Email Address",
                "Date of Birth",
            };

            foreach (var header in headers)
            {
                var cell = new PdfPCell(new Phrase(header, headerFont));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
            }

            var dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 9);
            var count = 0;
            foreach (var student in students)
            {
                count++;
                var dateOfBirth = student.DateOfBirth.ToString("yyyy-MM-dd");
                table.AddCell(new Phrase(count + ". ", dataFont));
                table.AddCell(new Phrase(student.StudentNumber, dataFont));
                table.AddCell(new Phrase(student.FirstName, dataFont));
                table.AddCell(new Phrase(student.LastName, dataFont));
                table.AddCell(new Phrase(student.EmailAddress, dataFont));
                table.AddCell(new Phrase(dateOfBirth, dataFont));
            }

            document.Add(table);
            document.Close();

            return stream.ToArray();
        }

        public byte[] ExportToExcel(List<Student> students)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Students");

            worksheet.Cell(1, 1).Value = "Student Number";
            worksheet.Cell(1, 2).Value = "First Name";
            worksheet.Cell(1, 3).Value = "Last Name";
            worksheet.Cell(1, 4).Value = "Email Address";
            worksheet.Cell(1, 5).Value = "Date of Birth";

            //Some Styles
            var headerRange = worksheet.Range(1, 1, 1, 5);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            for (int i = 0; i < students.Count; i++)
            {
                var row = i + 2;

                worksheet.Cell(row, 1).Value = students[i].StudentNumber;
                worksheet.Cell(row, 2).Value = students[i].FirstName;
                worksheet.Cell(row, 3).Value = students[i].LastName;
                worksheet.Cell(row, 4).Value = students[i].EmailAddress;
                worksheet.Cell(row, 5).Value = students[i].DateOfBirth;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
