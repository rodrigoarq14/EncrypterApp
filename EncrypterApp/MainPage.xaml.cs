using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using static Xamarin.Essentials.Permissions;

namespace EncrypterApp
{
    public partial class MainPage : ContentPage
    {
        public int[] numeros = new int[64];
        public string[] caracteres = new string[] {
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "V",
        "W",
        "X",
        "Y",
        "Z",
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        ".",
        ",",
        "!",
        "¡",
        "?",
        "$",
        "#",
        "@",
        "+",
        "-",
        "=",
        "*",
        "/",
        "_",
        ";",
        "<",
        ">",
        "[",
        "]",
        "{",
        "}",
        "\"",
        "'",
        "^",
        "&",
        " ",
        "(",
        ")"
        };
        string[] valores = new string[] {
        "000000",
        "000001",
        "000010",
        "000011",
        "000100",
        "000101",
        "000110",
        "000111",
        "001000",
        "001001",
        "001010",
        "001011",
        "001100",
        "001101",
        "001110",
        "001111",
        "010000",
        "010001",
        "010010",
        "010011",
        "010100",
        "010101",
        "010110",
        "010111",
        "011000",
        "011001",
        "011010",
        "011011",
        "011100",
        "011101",
        "011110",
        "011111",
        "100000",
        "100001",
        "100010",
        "100011",
        "100100",
        "100101",
        "100110",
        "100111",
        "101000",
        "101001",
        "101010",
        "101011",
        "101100",
        "101101",
        "101110",
        "101111",
        "110000",
        "110001",
        "110010",
        "110011",
        "110100",
        "110101",
        "110110",
        "110111",
        "111000",
        "111001",
        "111010",
        "111011",
        "111100",
        "111101",
        "111110",
        "111111"
        };

        static string DesplazarTextoNormal(string texto, int n)
        {
            string textoDesplazado = string.Empty;
            int longitudTexto = texto.Length;

            // Asegurarse de que n esté en el rango válido
            n = n % longitudTexto;

            // Obtener la parte desplazada del texto
            string parteDesplazada = texto.Substring(n);

            // Generar el texto desplazado
            textoDesplazado = parteDesplazada + texto.Substring(0, n);

            return textoDesplazado;
        }

        static string DesplazarTextoInverso(string texto, int n)
        {
            string textoDesplazado = string.Empty;
            int longitudTexto = texto.Length;

            // Asegurarse de que n esté en el rango válido
            n = n % longitudTexto;

            // Obtener la parte desplazada inversa del texto
            string parteDesplazada = texto.Substring(texto.Length - n);

            // Generar el texto desplazado inverso
            textoDesplazado = parteDesplazada + texto.Substring(0, texto.Length - n);

            return textoDesplazado;
        }

        static string FlipHorizontal(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static string[] SepararEnBloques(string texto, int tamanoBloque)
        {
            int cantidadBloques = texto.Length / tamanoBloque;
            string[] bloques = new string[cantidadBloques];

            for (int i = 0; i < cantidadBloques; i++)
            {
                bloques[i] = texto.Substring(i * tamanoBloque, tamanoBloque);
            }

            return bloques;
        }

        public MainPage()
        {
            InitializeComponent();
            for (int i = 0; i < numeros.Length; i++)
            {
                numeros[i] = i+1;
            }
        }

        private void Btn_vista_cifrar_clicked(object sender, EventArgs e)
        {
            frame_principal_1.IsVisible = false;
            frame_principal_2.IsVisible = false;
            frame_descifrar.IsVisible = false;
            frame_cifrar.IsVisible = true;
        }

        private void Btn_vista_descifrar_clicked(object sender, EventArgs e)
        {
            frame_principal_1.IsVisible = false;
            frame_principal_2.IsVisible = false;
            frame_cifrar.IsVisible = false;
            frame_descifrar.IsVisible = true;
        }

        private void Btn_cifrar_clicked(object sender, EventArgs e)
        {
            if (clave_cifrado.Text == "" || texto_cifrar.Text == "" || clave_cifrado.Text == null || texto_cifrar.Text == null)
            {
                DisplayAlert("Información", "El campo de clave o texto está vacio", "Aceptar");
            }
            else
            {
                if(clave_cifrado.Text.Length >= 3)
                {
                    //OBTENEMOS LOS VALORES I,J,K DE LA CLAVE
                    char[] claveCifrar = clave_cifrado.Text.ToCharArray();
                    int valorClaveI = Array.IndexOf(caracteres, claveCifrar[0].ToString());
                    valorClaveI = numeros[valorClaveI];
                    int valorClaveJ = Array.IndexOf(caracteres, claveCifrar[1].ToString());
                    valorClaveJ = numeros[valorClaveJ];
                    int valorClaveK = 0;
                    for (int i = 2; i < claveCifrar.Length; i++)
                    {
                        valorClaveK += numeros[Array.IndexOf(caracteres, claveCifrar[i].ToString())];
                    }
                    //OBTENEMOS LOS VALORES DEL TEXTO A CIFRAR
                    string[] valoresBinarios = new string[texto_cifrar.Text.Length];
                    for (int i = 0; i < texto_cifrar.Text.Length; i++)
                    {
                        valoresBinarios[i] = valores[Array.IndexOf(caracteres, texto_cifrar.Text[i].ToString())];
                    }
                    //Concatenamos en una sola cadena
                    string cadenaTodo = string.Join("", valoresBinarios);
                    // O1
                    //Realizamos desplazamiento de los caracteres(valorClaveI = 4) veces
                    cadenaTodo = DesplazarTextoNormal(cadenaTodo, valorClaveI);
                    //Separamos la cadena por bloques de 6 caracteres
                    valoresBinarios = SepararEnBloques(cadenaTodo, 6);
                    // O2
                    //Desplazamos cada bloque de cadena (valorClaveI + valorClaveJ) veces
                    for (int i = 0;i < valoresBinarios.Length; i++)
                    {
                        valoresBinarios[i] = DesplazarTextoNormal(valoresBinarios[i], (valorClaveI + valorClaveJ));
                    }
                    //Concatenamos en una sola cadena
                    cadenaTodo = string.Join("", valoresBinarios);
                    // O3
                    //Realizamos un Flip horizontal a la cadena
                    cadenaTodo = FlipHorizontal(cadenaTodo);
                    //Realizamos desplazamiento inverso(valorClaveI + valorClaveJ + valorClaveK) veces
                    cadenaTodo = DesplazarTextoInverso(cadenaTodo, (valorClaveI + valorClaveJ + valorClaveK));
                    //Separamos la cadena por bloques de 6 caracteres
                    valoresBinarios = SepararEnBloques(cadenaTodo, 6);
                    string txtCifrado = "";
                    for(int i = 0; i < valoresBinarios.Length; i++)
                    {
                        txtCifrado += caracteres[Array.IndexOf(valores, valoresBinarios[i].ToString())];
                    }
                    texto_cifrar.Text = txtCifrado;
                    //DisplayAlert("Información", claveCifrar.Length.ToString(), "Aceptar");
                }
                else
                {
                    DisplayAlert("Información", "La clave debe ser mayor a o igual a 3 caracteres", "Aceptar");
                }
            }
        }

        private void Btn_descifrar_clicked(object sender, EventArgs e)
        {
            if (clave_descifrado.Text == "" || texto_descifrar.Text == "" || clave_descifrado.Text == null || texto_descifrar.Text == null)
            {
                DisplayAlert("Información", "El campo de clave o texto está vacio", "Aceptar");
            }
            else
            {
                if (clave_descifrado.Text.Length >= 3)
                {
                    //OBTENEMOS LOS VALORES I,J,K DE LA CLAVE
                    char[] claveCifrar = clave_cifrado.Text.ToCharArray();
                    int valorClaveI = Array.IndexOf(caracteres, claveCifrar[0].ToString());
                    valorClaveI = numeros[valorClaveI];
                    int valorClaveJ = Array.IndexOf(caracteres, claveCifrar[1].ToString());
                    valorClaveJ = numeros[valorClaveJ];
                    int valorClaveK = 0;
                    for (int i = 2; i < claveCifrar.Length; i++)
                    {
                        valorClaveK += numeros[Array.IndexOf(caracteres, claveCifrar[i].ToString())];
                    }
                    //OBTENEMOS LOS VALORES DEL TEXTO A DESCIFRAR
                    string[] valoresBinarios = new string[texto_cifrar.Text.Length];
                    for (int i = 0; i < texto_cifrar.Text.Length; i++)
                    {
                        valoresBinarios[i] = valores[Array.IndexOf(caracteres, texto_cifrar.Text[i].ToString())];
                    }
                    //Concatenamos en una sola cadena
                    string cadenaTodo = string.Join("", valoresBinarios);
                    //Realizamos desplazamiento de los caracteres(valorClaveI + valorClaveJ + valorClaveK) veces
                    cadenaTodo = DesplazarTextoNormal(cadenaTodo, (valorClaveI + valorClaveJ + valorClaveK));
                    //Realizamos un Flip horizontal a la cadena
                    cadenaTodo = FlipHorizontal(cadenaTodo);
                    //Separamos la cadena por bloques de 6 caracteres
                    valoresBinarios = SepararEnBloques(cadenaTodo, 6);
                    //Desplazamiento inverso de cada bloque de cadena (valorClaveI + valorClaveJ) veces
                    for (int i = 0; i < valoresBinarios.Length; i++)
                    {
                        valoresBinarios[i] = DesplazarTextoInverso(valoresBinarios[i], (valorClaveI + valorClaveJ));
                    }
                    //Concatenamos en una sola cadena
                    cadenaTodo = string.Join("", valoresBinarios);
                    //Realizamos desplazamiento inverso de los caracteres (valorClaveI)
                    cadenaTodo = DesplazarTextoInverso(cadenaTodo, valorClaveI);
                    //Separamos la cadena por bloques de 6 caracteres
                    valoresBinarios = SepararEnBloques(cadenaTodo, 6);
                    string txtDesCifrado = "";
                    for (int i = 0; i < valoresBinarios.Length; i++)
                    {
                        txtDesCifrado += caracteres[Array.IndexOf(valores, valoresBinarios[i].ToString())];
                    }
                    texto_descifrar.Text = txtDesCifrado;
                }
                else
                {
                    DisplayAlert("Información", "La clave debe ser mayor a o igual a 3 caracteres", "Aceptar");
                }
            }
        }

        private void Btn_volver_clicked(object sender, EventArgs e)
        {
            frame_cifrar.IsVisible = false;
            frame_descifrar.IsVisible = false;
            frame_principal_1.IsVisible = true;
            frame_principal_2.IsVisible = true;
        }
    }
}
