using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ApplicationTestinMetricsLab1
{

    //базовый класс для формул, содержит в себе общие методы и поля для 
    //работы с формулами
    abstract class Formula
    {
        //формула, которую считает программа
        protected String _f;
        //решение формулы, заполняется после расщётов
        protected String _answer;
        //список операндов
        protected List<Formula> parts;
        //список операций
        protected List<char> act;

        //конструктор класса, при создании объекста должен получить:
        //f - решаемую формулу
        public Formula(String f)
        {
            this._f = f;
            this.parts = new List<Formula>();
            this.act = new List<char>();
            this.Normal();
            if (f[0] != '-') this.act.Add('+');
            this.main();
        }

        //метод в котором происхлдят основные действия
        //ничего не получает так, как берёт всё из полей своего объекта
        abstract protected void main();

        //возвращает результат решения формулы
        public String getResult()
        {
            return this._answer;
        }

        //при решении примера, он разбивается на составные части и этот метод определяет какой объект, нужно создать 
        //на входе:
        //f - подформула, которую мы анализируем
        //возвращает:
        //kind - символ обозначающий тип подформулы
        //H - присутсвует сложение или разность
        //S - присутсвует умножение или деление
        //L - есть возведение в степень
        //O - подформула в скобках или содержит только 1 число
        virtual protected char choise(String f)
        {
            char kind = 'n';
            char[] PM = { '+', '-' };
            char[] MD = { '*', '/' };
            char[] P = { '^' };
            int pm = this.Checker(f, PM), md = this.Checker(f, MD), p = this.Checker(f, P);
            if (pm >= 1) { kind = 'H'; }
            if (pm == 0 && md >= 1) { kind = 'S'; }
            if (pm == 0 && md == 0 && p > 0) { kind = 'L'; }
            if (pm == 0 && md == 0 && p == 0) { kind = 'O'; }
            switch (kind)
            {
                case 'H':
                    this.parts.Add(new HardFormula(f));
                    break;
                case 'S':
                    this.parts.Add(new SimpleFormula(f));
                    break;
                case 'L':
                    this.parts.Add(new LestFormula(f));
                    break;
                case 'O':
                    this.parts.Add(new OneDigitFormula(f));
                    break;
            }
            return kind;
        }

        //метод вычисляюищй результат
        //ничего не получает, так как пользуется полем _part и _acr
        //возвращает строку с ответом
        protected string summator()
        {
            string answer;
            double sum = 0;
            int j = 0;
            try
            {
                foreach (Formula i in this.parts)
                {
                    char kind = this.act.ElementAt(j);
                    int e = 1, zero = 0;
                    switch (kind)
                    {
                        case '-': sum -= Convert.ToDouble(i.getResult()); break;
                        case '+': sum += Convert.ToDouble(i.getResult()); break;
                        case '*': sum *= Convert.ToDouble(i.getResult()); break;
                        case '/': sum = sum / Convert.ToDouble(i.getResult()); if (Convert.ToDouble(i.getResult()) == 0) e /= zero; break;
                        case '^': sum = Math.Pow(sum, Convert.ToDouble(i.getResult())); break;
                    }
                    j++;
                }
                answer = sum.ToString();
            }
            catch (Exception e)
            {
                if (e.GetType().FullName != "DivideByZeroException")
                    answer = "Math Error";
                else answer = "Syntax Error";
            }
            return answer;
        }

        //считает количество символов g в f, не находящиеся в скобках
        //входные данные
        //f - подстрока
        //g - считаемые символы
        //возвращает:
        //result - количество символов
        protected int Checker(String f, char[] g)
        {
            int result = 0, s = 0;
            for (int i = 0; i < f.Length; i++)
            {
                s = (f[i] == '(') ? s + 1 : s;
                s = (f[i] == ')') ? s - 1 : s;
                for (int j = 0; j < g.Length && s == 0; j++)
                {
                    result = (f[i] == g[j]) ? result + 1 : result;
                }
            }
            return result;
        }

        //метод нормализующий формулу
        //вытаскивает формулу из скобок
        protected void Normal()
        {
            while (this._f[0] == '(' && this._f[this._f.Length - 1] == ')')
            {
                String n = "";
                for (int i = 1; i < this._f.Length - 1; i++)
                {
                    n += this._f[i];
                }
                this._f = n;
            }
        }
    }

    //объект делит формулу на слагемые
    class HardFormula : Formula
    {

        //операции, на которые делит формулу
        private char[] separator = { '+', '-' };

        //конструктор получает, формулу и передаёт её в базовый конструктор
        public HardFormula(String f) : base(f) { this._f += "_"; }

        //переопределённый основной метод, в нем всё так же выполняются основные действия
        protected override void main()
        {

            int s = 0;
            String parter = "";
            Formula x;
            for (int i = 0; i < this._f.Length; i++)
            {
                if (this._f[i] != '-' && this._f[i] != '+' && this._f[i] != '_')
                {
                    parter += this._f[i];
                    if (this._f[i] == '(') s++;
                    if (this._f[i] == ')') s--;
                }
                else
                {
                    if (s == 0)
                    {
                        this.choise(parter);
                        parter = "";
                        this.act.Add(this._f[i]);
                    }
                    else parter += this._f[i];
                }
            }
            this.choise(parter);
            this._answer = this.summator().ToString();
        }
    }

    //формула, что делится на множетели
    class SimpleFormula : Formula
    {
        //операции, на которые делит формулу
        private char[] separator = { '*', '/' };

        //конструктор получает, формулу и передаёт её в базовый конструктор
        public SimpleFormula(String f) : base(f) { this._f += "_"; }

        //переопределённый основной метод, в нем всё так же выполняются основные действия
        protected override void main()
        {
            int s = 0;
            String parter = "";
            for (int i = 0; i < this._f.Length; i++)
            {
                if (this._f[i] != '*' && this._f[i] != '/')
                {
                    parter += this._f[i];
                    if (this._f[i] == '(') s++;
                    if (this._f[i] == ')') s--;
                }
                else
                {
                    if (s == 0)
                    {
                        this.choise(parter);
                        parter = "";
                        this.act.Add(this._f[i]);
                    }
                    else parter += this._f[i];
                }
            }
            this.choise(parter);

            this._answer = this.summator().ToString();
        }
    }

    class LestFormula : Formula
    {
        //конструктор получает, формулу и передаёт её в базовый конструктор
        public LestFormula(string f) : base(f) { }

        //переопределённый основной метод, в нем всё так же выполняются основные действия
        protected override void main()
        {
            int s = 0;
            String parter = "";
            for (int i = 0; i < this._f.Length; i++)
            {
                if (this._f[i] != '^')
                {
                    parter += this._f[i];
                    if (this._f[i] == '(') s++;
                    if (this._f[i] == ')') s--;
                }
                else
                {
                    if (s == 0)
                    {
                        this.choise(parter);
                        parter = "";
                        this.act.Add(this._f[i]);
                    }
                    else parter += this._f[i];
                }
            }
            this.choise(parter);

            this._answer = this.summator().ToString();
        }

    }

    class OneDigitFormula : Formula
    {
        //конструктор получает, формулу и передаёт её в базовый конструктор
        public OneDigitFormula(String f) : base(f) { }

        //переопределённый основной метод, в нем всё так же выполняются основные действия
        protected override void main()
        {
            char kind = this.choise(this._f);
            if (kind != 'O')
            {
                this._answer = this.parts.ElementAt(0).getResult();
            }
        }

        override protected char choise(String f)
        {
            char kind = 'n';
            char[] PM = { '+', '-' };
            char[] MD = { '*', '/' };
            char[] P = { '^' };
            if (this.Checker(f, PM) >= 1) { kind = 'H'; }
            if (this.Checker(f, PM) == 0 && this.Checker(f, MD) >= 1) { kind = 'S'; }
            if (this.Checker(f, PM) == 0 && this.Checker(f, PM) == 0 && this.Checker(f, P) == 0) { kind = 'O'; }
            if (this.Checker(f, PM) == 0 && this.Checker(f, PM) == 0 && this.Checker(f, P) > 0) { kind = 'L'; }
            switch (kind)
            {
                case 'H':
                    this.parts.Add(new HardFormula(f));
                    break;
                case 'S':
                    this.parts.Add(new SimpleFormula(f));
                    break;
                case 'L':
                    this.parts.Add(new LestFormula(f));
                    break;
                case 'O':
                    this._answer = this.R(this._f);
                    break;
            }
            return kind;
        }

        //заменяет сокращения на числа (pi - число пи, е - экспонента)
        //на входе: f - сокращение
        //на выходе: расшифровка, или строка f, если расшифровать нельзя
        private string R(string f)
        {
            if (f == "pi") { return Math.PI.ToString(); }
            if (f == "e") { return Math.E.ToString(); }
            else { return f; }
        }
    }
}

