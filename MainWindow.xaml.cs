using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2742117_2730729_2744585_2742117_UA1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentInput = "0";
        private string operation = "";
        private double firstNumber = 0;
        private double secondNumber = 0;
        private bool isNewOperation = true;
        private bool isFunctionOperation = false;

        public MainWindow()
            {
                InitializeComponent();
                UpdateDisplay();
            }

            /// <summary>
            /// Met à jour l'affichage de la calculatrice
            /// </summary>
            private void UpdateDisplay()
            {
                txtResult.Text = currentInput;
                txtOperation.Text = operation;
            }

            /// <summary>
            /// Gestion des clics sur les boutons chiffres (0-9)
            /// </summary>
            private void NumberButton_Click(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                string number = button.Content.ToString();

                if (currentInput == "0" || isNewOperation || isFunctionOperation)
                {
                    currentInput = number;
                    isNewOperation = false;
                    isFunctionOperation = false;
                }
                else
                {
                    currentInput += number;
                }

                UpdateDisplay();
            }

            /// <summary>
            /// Gestion des clics sur les opérateurs (+, -, ×, ÷)
            /// </summary>
            private void OperatorButton_Click(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                string newOperator = button.Content.ToString();

                if (!isNewOperation)
                {
                    if (!string.IsNullOrEmpty(operation))
                    {
                        CalculateResult();
                    }
                firstNumber = double.Parse(currentInput);
                }

                operation = newOperator;
                isNewOperation = true;
                UpdateDisplay();
            }

            /// <summary>
            /// Gestion du clic sur le bouton égal (=)
            /// </summary>
            private void EqualsButton_Click(object sender, RoutedEventArgs e)
            {
                if (!string.IsNullOrEmpty(operation) && !isNewOperation)
                {
                    CalculateResult();
                    operation = "";
                    isNewOperation = true;
                    UpdateDisplay();
                }
            }

            /// <summary>
            /// Effectue le calcul selon l'opération en cours
            /// </summary>
            private void CalculateResult()
            {
                try
                {
                    secondNumber = double.Parse(currentInput);
                    double result = 0;

                    switch (operation)
                    {
                        case "+":
                            result = firstNumber + secondNumber;
                            break;
                        case "-":
                            result = firstNumber - secondNumber;
                            break;
                        case "×":
                            result = firstNumber * secondNumber;
                            break;
                        case "÷":
                            if (secondNumber == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            result = firstNumber / secondNumber;
                            break;
                    }

                    currentInput = result.ToString();
                    operation = $"{firstNumber} {operation} {secondNumber} =";
                }
                catch (DivideByZeroException)
                {
                    currentInput = "Error";
                    operation = "Division par zéro!";
                }
                catch (Exception)
                {
                    currentInput = "Error";
                    operation = "Erreur de calcul";
                }
            }

            /// <summary>
            /// Gestion des fonctions trigonométriques (Sin, Cos, Tan, Arcsin, Arccos, Arctan)
            /// </summary>
            private void FunctionButton_Click(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                string function = button.Content.ToString();
                double inputValue = double.Parse(currentInput);
                double result = 0;
                double radians = inputValue * Math.PI / 180; // Conversion en radians

                try
                {
                    switch (function)
                    {
                        case "Sin":
                            result = Math.Sin(radians);
                            operation = $"Sin({inputValue})";
                            break;
                        case "Cos":
                            result = Math.Cos(radians);
                            operation = $"Cos({inputValue})";
                            break;
                        case "Tan":
                            result = Math.Tan(radians);
                            operation = $"Tan({inputValue})";
                            break;
                        case "Arcsin":
                            if (inputValue >= -1 && inputValue <= 1)
                            {
                                result = Math.Asin(inputValue) * 180 / Math.PI;
                                operation = $"Arcsin({inputValue})";
                            }
                            else
                            {
                                throw new ArgumentException("Valeur hors domaine");
                            }
                            break;
                        case "Arccos":
                            if (inputValue >= -1 && inputValue <= 1)
                            {
                                result = Math.Acos(inputValue) * 180 / Math.PI;
                                operation = $"Arccos({inputValue})";
                            }
                            else
                            {
                                throw new ArgumentException("Valeur hors domaine");
                            }
                            break;
                        case "Arctan":
                            result = Math.Atan(inputValue) * 180 / Math.PI;
                            operation = $"Arctan({inputValue})";
                            break;
                    }

                    currentInput = result.ToString();
                    isFunctionOperation = true;
                    UpdateDisplay();
                }
                catch (Exception ex)
                {
                    currentInput = "Error";
                    operation = ex.Message;
                    UpdateDisplay();
                }
            }

            /// <summary>
            /// Gestion des constantes (π, e)
            /// </summary>
            private void ConstantButton_Click(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                string constant = button.Content.ToString();

                switch (constant)
                {
                    case "π":
                        currentInput = Math.PI.ToString();
                        operation = "π";
                        break;
                    case "e":
                        currentInput = Math.E.ToString();
                        operation = "e";
                        break;
                }

                isFunctionOperation = true;
                UpdateDisplay();
            }

            /// <summary>
            /// Gestion du point décimal
            /// </summary>
            private void DecimalButton_Click(object sender, RoutedEventArgs e)
            {
                if (!currentInput.Contains("."))
                {
                    currentInput += ".";
                    UpdateDisplay();
                }
            }

            /// <summary>
            /// Gestion du changement de signe (+/-)
            /// </summary>
            private void PlusMinusButton_Click(object sender, RoutedEventArgs e)
            {
                if (currentInput != "0")
                {
                    if (currentInput.StartsWith("-"))
                    {
                        currentInput = currentInput.Substring(1);
                    }
                    else
                    {
                        currentInput = "-" + currentInput;
                    }
                    UpdateDisplay();
                }
            }

            /// <summary>
            /// Efface tout (bouton C)
            /// </summary>
            private void CButton_Click(object sender, RoutedEventArgs e)
            {
                currentInput = "0";
                operation = "";
                firstNumber = 0;
                secondNumber = 0;
                isNewOperation = true;
                UpdateDisplay();
            }

            /// <summary>
            /// Efface l'entrée courante (bouton CE)
            /// </summary>
            private void CEButton_Click(object sender, RoutedEventArgs e)
            {
                currentInput = "0";
                UpdateDisplay();
            }

            /// <summary>
            /// Efface le dernier caractère (bouton Back)
            /// </summary>
            private void BackButton_Click(object sender, RoutedEventArgs e)
            {
                if (currentInput.Length > 1)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                }
                else
                {
                    currentInput = "0";
                }
                UpdateDisplay();
            }
        }
    }
   