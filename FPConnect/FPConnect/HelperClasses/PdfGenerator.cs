using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using static FPConnect.view.Pages.AlumnosSubPages.Archivados;

namespace FPConnect.HelperClasses { 
    public class MiembroModel
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        // Otras propiedades que puedas tener
    }

    public class PdfGenerator
    {
        public static void GenerarPdfMiembros(IEnumerable<Member> miembros, string titulo)
        {
            // Crear diálogo para guardar archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivo PDF (*.pdf)|*.pdf",
                Title = "Lista de alumnos archivados",
                FileName = "ListaMiembrosArchivados " +DateTime.Now.ToString().Replace("/","_").Replace(":","_") + ".pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Configuración del documento
                    Document documento = new Document(PageSize.A4, 50, 50, 50, 50);
                    PdfWriter writer = PdfWriter.GetInstance(documento, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    documento.Open();

                    // Logo del centro
                    string urlImagen = "https://iesmaestredecalatrava.es/wp-content/uploads/logotipo-ies-maestredecalatrava-1.png";
                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(new Uri(urlImagen));
                    imagen.ScaleToFit(100f, 90f);
                    imagen.Alignment = Element.ALIGN_RIGHT;

                    documento.Add(imagen);


                    // Titulo
                    Font fontTitulo = new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD);
                    Paragraph parrafoTitulo = new Paragraph(titulo, fontTitulo);
                    parrafoTitulo.Alignment = Element.ALIGN_CENTER;
                    parrafoTitulo.SpacingAfter = 20;
                    
                    documento.Add(parrafoTitulo);

                    
                    // Fecha actual
                    Font fontFecha = new Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC);
                    Paragraph parrafoFecha = new Paragraph($"Generado el: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", fontFecha);
                    parrafoFecha.Alignment = Element.ALIGN_RIGHT;
                    parrafoFecha.SpacingAfter = 20;
                    documento.Add(parrafoFecha);

                    // Tabla
                    PdfPTable tabla = new PdfPTable(4); // 4 columnas
                    tabla.WidthPercentage = 100;
                    tabla.SetWidths(new float[] { 2f, 3f, 4f, 2.5f }); // Proporciones de la columna

                    // Estilo de cabecera
                    Font fontCabecera = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE);
                    PdfPCell celdaCabecera = new PdfPCell(new Phrase("Cabecera", fontCabecera));
                    celdaCabecera.BackgroundColor = new BaseColor(51, 144, 255); // Color parecido a #3090ff (azul del menu)
                    celdaCabecera.HorizontalAlignment = Element.ALIGN_CENTER;
                    celdaCabecera.Padding = 8;

                    // Añadir cabeceras
                    celdaCabecera.Phrase = new Phrase("Nombre", fontCabecera);
                    tabla.AddCell(celdaCabecera);

                    celdaCabecera.Phrase = new Phrase("Apellidos", fontCabecera);
                    tabla.AddCell(celdaCabecera);

                    celdaCabecera.Phrase = new Phrase("Correo", fontCabecera);
                    tabla.AddCell(celdaCabecera);

                    celdaCabecera.Phrase = new Phrase("Teléfono", fontCabecera);
                    tabla.AddCell(celdaCabecera);

                    // Estilo de las celdas de datos
                    Font fontDatos = new Font(Font.FontFamily.HELVETICA, 10);
                    BaseColor colorFilaAlterna = new BaseColor(240, 240, 240);

                    // Añadir filas de datos
                    int contador = 0;
                    foreach (var miembro in miembros)
                    {
                        // Alternar color de fondo para mejor legibilidad
                        BaseColor colorFondo = (contador % 2 == 0) ? BaseColor.WHITE : colorFilaAlterna;

                        PdfPCell celda = new PdfPCell(new Phrase(miembro.Name, fontDatos));
                        celda.BackgroundColor = colorFondo;
                        celda.Padding = 6;
                        tabla.AddCell(celda);

                        celda = new PdfPCell(new Phrase(miembro.Position, fontDatos));
                        celda.BackgroundColor = colorFondo;
                        tabla.AddCell(celda);

                        celda = new PdfPCell(new Phrase(miembro.Email, fontDatos));
                        celda.BackgroundColor = colorFondo;
                        tabla.AddCell(celda);

                        celda = new PdfPCell(new Phrase(miembro.Phone, fontDatos));
                        celda.BackgroundColor = colorFondo;
                        tabla.AddCell(celda);

                        contador++;
                    }

                    // Añadir la tabla al documento
                    documento.Add(tabla);

                    // Añadir información de resumen
                    Paragraph resumen = new Paragraph($"Total de miembros: {contador}", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD));
                    resumen.Alignment = Element.ALIGN_RIGHT;
                    resumen.SpacingBefore = 15;
                    documento.Add(resumen);

                    // Pie de pagina
                    Paragraph footer = new Paragraph("FPConnect - Sistema de Gestión de Alumnos", new Font(Font.FontFamily.HELVETICA, 8));
                    footer.Alignment = Element.ALIGN_CENTER;
                    footer.SpacingBefore = 30;
                    documento.Add(footer);

                    documento.Close();
                    MessageBox.Show("El PDF se ha generado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}