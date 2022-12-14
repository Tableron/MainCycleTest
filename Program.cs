using System;
using System.Windows.Forms;
using System.Threading;

namespace MainCycleTest
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            // Необходимо выполнить перед содание любых объектов
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Поток с циклом
            MainCycle mainCycle = new MainCycle(); // mc общий для обоих потоков
            Thread mcThread = new Thread(mainCycle.Start); // в новом потоке будет запускаться Start у mainCycle
            mcThread.IsBackground = false; // Прекращение этого потока прекращает программу
            mcThread.Name = "mcThread";
            mcThread.Start(); // запуск потока

            // Поток для формы
            Form1 form1 = new Form1();
            form1.SetMainCycle(mainCycle); // ссылка на ms для обращений из формы
            FormStarter formStarter = new FormStarter(form1); // Класс-прослойка для запуска формы в отдельном потоке
            Thread formThread = new Thread(formStarter.Start);
            formThread.IsBackground = true; // Прекращение этого потока не ведёт к прекращению программы
                                            // Программы может прекратить работу, если этот поток работает
            formThread.Name = "formThread";
            formThread.Start(); // запуск потока
        }
    }

    class FormStarter
    {
        private Form1 _form;
        public FormStarter(Form1 form)
        {
            _form = form;
        }

        public void Start()
        {
            Application.Run(_form); // запуск формы
        }
    }

    class MainCycle
    {
        public int Data { get; set; } // Это свойство форма запрашивает у цикла
        private Form1 _form;

        private bool _contin = true;

        public MainCycle()
        {
            Data = 0;
        }

        public void SetForm(Form1 form)
        {
            _form = form;
        }

        public void Start()
        {
            Console.WriteLine($"Name={Thread.CurrentThread.Name}");
            while(_contin)
            {
                // какая-то логика
                for (int i = 0; i < 10; i++)
                {
                    Data = i;
                    Console.WriteLine(i);

                    Thread.Sleep(100); // задержка 100 мс
                }
            }
        }

        // Метод предназначен для вызова из потока формы
        public void Stop()
        {
            Console.WriteLine($"Name={Thread.CurrentThread.Name}");
            _contin = false;
        }
    }

}
