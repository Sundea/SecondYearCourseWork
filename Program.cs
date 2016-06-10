using CourseWork.Controller;
using CourseWork.Model;
using CourseWork.View;

namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            ViewComponent view = new ViewComponent();
            Balance model = new Balance();

            ControllerComponent controller = new ControllerComponent(view, model);

            controller.DoActions();

        }
    }
}
