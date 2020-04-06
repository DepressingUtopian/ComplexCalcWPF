using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculatorWPF
{
    public class ControlFacade
    {
        private Editor editor;
        private Executor executor;
        private History history;

        internal History History { get => history; set => history = value; }

        public ControlFacade()
        {
            this.editor = new Editor();
            this.executor = new Executor();
            this.history = new History();
        }
        public ControlFacade(Editor editor, Executor executor)
        {
            this.editor = editor;
            this.executor = executor;
        }
        public string DoEdit(int id)
        {

            if (id == 14)
            {
                var resultExecute = this.executor.ExecuteExpression(this.editor.DoEdit(id));
                History.AddRecord(this.editor.Expression,resultExecute);
                this.editor.Expression = resultExecute;
                return this.editor.Expression;
            }
            if (id == 17)
            {
                executor.Clear();
                return this.editor.DoEdit(id);
            }
            else
                return this.editor.DoEdit(id);
        }
        public string DoCommand(string operation, int value = 0)
        {
            var expression  = this.executor.ExecuteExpression(this.editor.Number + "=",true);
            var resultExpression  = this.executor.ExecuteCommand(operation, value);
            History.AddRecord(expression, operation, resultExpression);

            return resultExpression;
        }
        public void UpdateEditorExpression(string expression)
        {
            this.editor.Expression = expression;
        }
        public void Clear()
        {
            editor.Clear();
            executor.Clear();
        }
    }
}
