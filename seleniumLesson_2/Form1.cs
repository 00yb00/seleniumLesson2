using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace seleniumLesson_2
{
    public partial class Form1 : Form
    {
        string url = "https://accounts.google.com/";

        public Form1()
        {
            InitializeComponent();
        }
        public void Puse(int mili = 10000)
        {
            System.Threading.Thread.Sleep(mili);
        }
        public void emailFunc(string email, string password)
        {
            using (IWebDriver webDriver = new ChromeDriver())
            {
                //wait until the page load 30 sec
                webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                //try to found the element afew times
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

                webDriver.Navigate().GoToUrl(url);
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                //mail input 
                IWebElement emailElement = wait.Until<IWebElement>((a) => { return a.FindElement(By.CssSelector("input[type='email']")); });
                emailElement.SendKeys(email);
                // confirm button
                IWebElement sendButton = wait.Until<IWebElement>((a) => { return a.FindElement(By.CssSelector("div[jsname='Njthtb']")); });
                if (sendButton != null)
                {
                    sendButton.Click();
                     new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                    //erorr massage div
                    IWebElement check =null;
                    try
                    {
                        //if the erorr message exist
                        check = webDriver.FindElement(By.CssSelector("span[class='jibhHc']"));

                    }
                    catch (Exception ex)
                    {
                       
                        Console.WriteLine(ex.Message.ToString());

                    }
                    if (check != null)
                    {
                        Console.WriteLine("the mail doesnot exist or unvalid");
                        check = null;
                    }
                    if (check == null && password != "")
                    { /*Puse();*/
                        IWebElement passwordElement = wait.Until<IWebElement>((a) => { return a.FindElement(By.CssSelector("input[type='password']")); });
                       
                        passwordElement.SendKeys(password);

                        // confirm button
                        IWebElement sendButton2 = wait.Until<IWebElement>((a) => { return a.FindElement(By.CssSelector("div[jsname='Njthtb']")); });
                        sendButton2.Click();
                        //Puse();
                        IWebElement check2=null;
                        try
                        {//the user home page 
                         //try to connect the user
                            check2= webDriver.FindElement(By.CssSelector("div[jsname='a5jDZb']"));
                        }
                        catch (Exception ex)
                        {
                            //if its faild 
                            //its mean the password is wrong
                            Console.WriteLine(ex.Message.ToString());
                        }

                        if (check2 != null)
                        {
                            Console.WriteLine("the process successed");
                        }
                        else
                        {
                            Console.WriteLine("the password is anvalid");
                        }
                    }
                    else
                    {
                        webDriver.Close();

                    }
                }
                else
                {
                    Console.WriteLine(" not found button");
                }

            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            emailFunc("abcdxiez10213451@gmail.com","");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            emailFunc("abcvvvv441@gmail.com", "");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            emailFunc("abcvvvv441@gmail.com", "123456ZA");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            emailFunc("abcvvvv441@gmail.com", "123456abcv");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            emailFunc("abcdxi.com", "");

        }
    }
}
