using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using FPConnect.domain;

namespace FPConnect.persistence.Manages
{
    internal class CentroManage
    {
        private ObservableCollection<Centro> listaCentros { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();
        public CentroManage()
        {
            listaCentros = new ObservableCollection<Centro>();
        }

        public Centro LeerCentroPorId(int id)
        {
            Centro centro = null;
            db = DBBroker.ObtenerAgente();
            var resultado = db.LeerConParametros("SELECT id_centro,nombre,direccion,horario,telefono,logo FROM fpc.centros WHERE id_centro = @id;", new Dictionary<string, object> { { "@id", id } });
            foreach (ObservableCollection<Object> c in resultado)
            {
                // Crear el centro con datos básicos
                centro = new Centro(
                    int.Parse(c[0].ToString()), //id
                    c[1].ToString(), //nombre
                    c[2].ToString(), //direccion
                    c[3].ToString(), //horario
                    c[4].ToString()); //telefono

                // Manejar el logo como BitmapImage
                if (c[5] != null && c[5] != DBNull.Value)
                {
                    try
                    {
                        // Si es byte[]
                        if (c[5] is byte[] blobBytes)
                        {
                            centro.logo = ConvertirBytesAImagen(blobBytes);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al convertir logo a imagen: {ex.Message}");
                    }
                }
            }
            return centro;
        }

        /// <summary>
        /// Convierte un array de bytes en un objeto BitmapImage
        /// </summary>
        public BitmapImage ConvertirBytesAImagen(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                return null;

            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    memoryStream.Position = 0;
                    bitmapImage.BeginInit();
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.UriSource = null;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze(); 
                }

                return bitmapImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al convertir bytes a imagen: {ex.Message}");
                return null;
            }
        }
    }
}
