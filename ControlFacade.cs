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

        public ControlFacade()
        {
            this.editor = new Editor();
            this.executor = new Executor();
        }
        public ControlFacade(Editor editor, Executor converter)
        {
            this.editor = editor;
            this.executor = converter;
        }
        public string DoEdit(int id)
        {

            if (id == 14)
            {
                this.editor.Expression = this.executor.ExecuteExpression(this.editor.DoEdit(id));
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
        public string DoCommand(string id, int value = 0)
        {
            this.executor.ExecuteExpression(this.editor.Number + "=",true);
            return  this.executor.ExecuteCommand(id, value);
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
