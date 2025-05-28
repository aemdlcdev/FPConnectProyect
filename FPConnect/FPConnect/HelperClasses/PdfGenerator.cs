using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using FPConnect.domain;
using System.Linq;

namespace FPConnect.HelperClasses
{
    public class MiembroModel
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Empresa { get; set; }
        
    }

    public class PdfGenerator
    {
        public static void GenerarPdfAlumnos(ObservableCollection<Alumno> alumnos, string titulo)
        {
            // Crear diálogo para guardar archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivo PDF (*.pdf)|*.pdf",
                Title = titulo,
                FileName = "ListaAlumnos_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Configuración del documento
                    Document documento = new Document(PageSize.A4, 50, 50, 50, 50);
                    PdfWriter writer = PdfWriter.GetInstance(documento, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    documento.Open();

                    // Intentar añadir logo
                    try
                    {
                        // Intenta cargar la imagen desde un archivo local primero (preferible)
                        string localImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo.png");
                        iTextSharp.text.Image imagen;

                        if (File.Exists(localImagePath))
                        {
                            imagen = iTextSharp.text.Image.GetInstance(localImagePath);
                        }
                        else
                        {
                            // Si no encuentra imagen local, crea un texto alternativo
                            Paragraph logoText = new Paragraph("FPConnect", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                            logoText.Alignment = Element.ALIGN_RIGHT;
                            documento.Add(logoText);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Si hay error con la imagen, simplemente continuamos sin el logo
                        Console.WriteLine($"No se pudo cargar el logo: {ex.Message}");
                    }

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
                    PdfPTable tabla = new PdfPTable(5); // 5 columnas
                    tabla.WidthPercentage = 100;
                    tabla.SetWidths(new float[] { 1f, 2f, 3f, 4f, 2f }); // Proporciones de la columna

                    // Estilo de cabecera
                    Font fontCabecera = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE);
                    PdfPCell celdaCabecera = new PdfPCell(new Phrase("Cabecera", fontCabecera));
                    celdaCabecera.BackgroundColor = new BaseColor(51, 144, 255); // Color parecido a #3090ff (azul del menu)
                    celdaCabecera.HorizontalAlignment = Element.ALIGN_CENTER;
                    celdaCabecera.Padding = 8;

                    // Añadir cabeceras
                    celdaCabecera.Phrase = new Phrase("ID", fontCabecera);
                    tabla.AddCell(celdaCabecera);

                    celdaCabecera.Phrase = new Phrase("Nombre", fontCabecera);
                    tabla.AddCell(celdaCabecera);

                    celdaCabecera.Phrase = new Phrase("Apellidos", fontCabecera);
                    tabla.AddCell(celdaCabecera);

                    celdaCabecera.Phrase = new Phrase("Email", fontCabecera);
                    tabla.AddCell(celdaCabecera);

                    celdaCabecera.Phrase = new Phrase("Empresa", fontCabecera);
                    tabla.AddCell(celdaCabecera);

                    // Estilo de las celdas de datos
                    Font fontDatos = new Font(Font.FontFamily.HELVETICA, 10);
                    BaseColor colorFilaAlterna = new BaseColor(240, 240, 240);

                    // Añadir filas de datos
                    int contador = 0;
                    foreach (var alumno in alumnos)
                    {
                        // Alternar color de fondo para mejor legibilidad
                        BaseColor colorFondo = (contador % 2 == 0) ? BaseColor.WHITE : colorFilaAlterna;

                        PdfPCell celda = new PdfPCell(new Phrase(alumno.id_alumno.ToString(), fontDatos));
                        celda.BackgroundColor = colorFondo;
                        celda.Padding = 6;
                        tabla.AddCell(celda);

                        celda = new PdfPCell(new Phrase(alumno.nombre ?? "", fontDatos));
                        celda.BackgroundColor = colorFondo;
                        tabla.AddCell(celda);

                        celda = new PdfPCell(new Phrase(alumno.apellidos ?? "", fontDatos));
                        celda.BackgroundColor = colorFondo;
                        tabla.AddCell(celda);

                        celda = new PdfPCell(new Phrase(alumno.email ?? "", fontDatos));
                        celda.BackgroundColor = colorFondo;
                        tabla.AddCell(celda);

                        // Empresa
                        string nombreEmpresa = "Sin empresa";
                        if (alumno.InfoAdicional != null && alumno.InfoAdicional.ContainsKey("nombre_empresa"))
                        {
                            var infoEmpresa = alumno.InfoAdicional["nombre_empresa"];
                            if (infoEmpresa != null)
                            {
                                nombreEmpresa = infoEmpresa.ToString();
                            }
                        }
                        celda = new PdfPCell(new Phrase(nombreEmpresa, fontDatos));
                        celda.BackgroundColor = colorFondo;
                        tabla.AddCell(celda);

                        contador++;
                    }

                    // Añadir la tabla al documento
                    documento.Add(tabla);

                    // Añadir información de resumen
                    Paragraph resumen = new Paragraph($"Total de alumnos: {contador}", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD));
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
                    Console.WriteLine($"Error al generar el PDF: {ex.Message}");
                }
            }
        }

    }
}