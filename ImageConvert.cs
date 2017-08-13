/*
 * Uma simples class responsavel por converter imagem para string em base64 para que possa ser armazenado 
 * em banco de dados em campos do tipo varchar.
 * Autor: Wesdras Alves
 * Site: wesdras.com.br
 * 
 * Exemplo de Uso:
 *  - Convertendo Imagem para Base64 
 *          > string _imgBase64 = (new ImageConvert()).ImageToBase64(@"C:\1.png");
 *          
 *  - Convertendo StringBase64 para Imagem 
 *          > pictureBox1.Image = (new ImageConvert()).Base64ToImage(_imgBase64);
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

public class ImageConvert
{
    public string Error
    {
        get;
        private set;
    }

    /// <summary>
    /// Metodo responsavel por converter imagem passada por caminho físico 
    /// em uma string usando base64
    /// </summary>
    /// <param name="pPathImage">Caminho da imagem</param>
    /// <returns>Imagem em string</returns>
    public string ImageToBase64(string pPathImage)
    {
        string base64String = string.Empty;

        try
        {
            //Carrega imagem em um stream
            using (FileStream fs = new FileStream(pPathImage, FileMode.Open))
            {
                //Criando byte array do tamanho da imagem 
                byte[] imageBytes = new byte[fs.Length];

                //Copiando dados do stream para o byte array
                fs.Read(imageBytes, 0, imageBytes.Length);

                // Convertendo byte array para string Base 64 
                base64String = Convert.ToBase64String(imageBytes);

            }
        }
        catch (Exception ex)
        {
            //Armazena o erro na Error caso ocorra algum erro a funcao retornara vazia
            //e a propriedade Error estara preenchida com o erro ocorrido.
            this.Error = ex.Message;
        }

        return base64String;
    }

    /// <summary>
    /// Converte uma string Base64 em imagem novamente para que possa ser exibida novamente
    /// </summary>
    /// <param name="base64String">String em Base64</param>
    /// <returns>Image</returns>
    public Image Base64ToImage(string pBase64String)
    {
        Image _image = null;
        try
        {
            //Converte a string em base64 passada em um byte array
            byte[] _imageBytes = Convert.FromBase64String(pBase64String);

            //Carrega um stream com o byte array
            using (var _ms = new MemoryStream(_imageBytes, 0, _imageBytes.Length))
            {
                //Convertendo Stream em Imagem
                _image = Image.FromStream(_ms, true);
            }

        }
        catch (Exception ex)
        {
            //Guarda o erro caso ocorra alguma falha, o metodo retorna null 
            //e a propriedade Error estara preenchida com o erro ocorrido.
            this.Error = ex.Message;
        }

        return _image;
    }
}
