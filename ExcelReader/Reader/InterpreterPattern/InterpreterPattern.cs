﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameInnvoation.ExcelReader.InterpreterPattern
{
    public class InterpreterPattern
    {
        public static void Interpreter(string[] args)
        {
            string roman = "五亿七千三百零二万六千四百五十二";
            //73006452
            //分解：((五)亿)((七千)(三百)(零)(二)万)
            //((六千)(四百)(五十)(二))
         
            Context context = new Context(roman);
            ArrayList tree = new ArrayList();
         
            tree.Add(new GeExpression());
            tree.Add(new ShiExpression());
            tree.Add(new BaiExpression());
            tree.Add(new QianExpression());
            tree.Add(new WanExpression());
            tree.Add(new YiExpression());
         
            foreach (Expression exp in tree)
            {
                exp.Interpreter(context);
            }
     
            Debug.LogFormat($"Context.Data:{context.Data}");
        }
    }
    
              

    public abstract class Expression
    {
        protected Dictionary<string, int> table = new Dictionary<string, int>(9);

        protected Expression()
        {
            table.Add("一", 1);
            table.Add("二", 2);
            table.Add("三", 3);
            table.Add("四", 4);
            table.Add("五", 5);
            table.Add("六", 6);
            table.Add("七", 7);
            table.Add("八", 8);
            table.Add("九", 9);
        }

        public virtual void Interpreter(Context context)
        {
            if (context.Statement.Length == 0)
            {
                return;
            }

            foreach (string key in table.Keys)
            {
                int value = table[key];

                if (context.Statement.EndsWith(key + GetPostFix()))
                {
                    context.Data += value * this.Multiplier();
                    context.Statement = context.Statement.Substring(0, context.Statement.Length - this.GetLength());
                }

                if (context.Statement.EndsWith("零"))
                {
                    context.Statement = context.Statement.Substring(0, context.Statement.Length - 1);
                }
            }
        }

        public abstract string GetPostFix();

        public abstract int Multiplier();

        //这个可以通用，但是对于个位数字例外，所以用虚方法
        public virtual int GetLength()
        {
            return this.GetPostFix().Length + 1;
        }
    }

    //个位表达式
    public sealed class GeExpression : Expression
    {
        public override string GetPostFix()
        {
            return "";
        }

        public override int Multiplier()
        {
            return 1;
        }

        public override int GetLength()
        {
            return 1;
        }
    }

    //十位表达式
    public sealed class ShiExpression : Expression
    {
        public override string GetPostFix()
        {
            return "十";
        }

        public override int Multiplier()
        {
            return 10;
        }
    }

    //百位表达式
    public sealed class BaiExpression : Expression
    {
        public override string GetPostFix()
        {
            return "百";
        }

        public override int Multiplier()
        {
            return 100;
        }
    }

    //千位表达式
    public sealed class QianExpression : Expression
    {
        public override string GetPostFix()
        {
            return "千";
        }

        public override int Multiplier()
        {
            return 1000;
        }
    }

    //万位表达式
    public sealed class WanExpression : Expression
    {
        public override string GetPostFix()
        {
            return "万";
        }

        public override int Multiplier()
        {
            return 10000;
        }

        public override void Interpreter(Context context)
        {
            if (context.Statement.Length == 0)
            {
                return;
            }

            ArrayList tree = new ArrayList();

            tree.Add(new GeExpression());
            tree.Add(new ShiExpression());
            tree.Add(new BaiExpression());
            tree.Add(new QianExpression());

            foreach (string key in table.Keys)
            {
                if (context.Statement.EndsWith(GetPostFix()))
                {
                    int temp = context.Data;
                    context.Data = 0;

                    context.Statement = context.Statement.Substring(0, context.Statement.Length - this.GetLength());

                    foreach (Expression exp in tree)
                    {
                        exp.Interpreter(context);
                    }

                    context.Data = temp + context.Data * this.Multiplier();
                }
            }
        }
    }

    //亿位表达式
    public sealed class YiExpression : Expression
    {
        public override string GetPostFix()
        {
            return "亿";
        }

        public override int Multiplier()
        {
            return 100000000;
        }

        public override void Interpreter(Context context)
        {
            ArrayList tree = new ArrayList();

            tree.Add(new GeExpression());
            tree.Add(new ShiExpression());
            tree.Add(new BaiExpression());
            tree.Add(new QianExpression());
            tree.Add(new WanExpression());

            foreach (string key in table.Keys)
            {
                if (context.Statement.EndsWith(GetPostFix()))
                {
                    int temp = context.Data;
                    context.Data = 0;
                    context.Statement = context.Statement.Substring(0, context.Statement.Length - this.GetLength());

                    foreach (Expression exp in tree)
                    {
                        exp.Interpreter(context);
                    }

                    context.Data = temp + context.Data * this.Multiplier();
                }
            }
        }
    }

    //环境上下文
    public sealed class Context
    {
        private string _statement;
        private int _data;

        public Context(string statement)
        {
            this._statement = statement;
        }

        public string Statement
        {
            get { return this._statement; }
            set { this._statement = value; }
        }

        public int Data
        {
            get { return this._data; }
            set { this._data = value; }
        }
    }
}