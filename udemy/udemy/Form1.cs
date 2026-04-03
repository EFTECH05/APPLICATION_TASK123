namespace udemy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void EmployeeName_Click(object sender, EventArgs e)
        {

        }

        private void EmployeePosition_Click(object sender, EventArgs e)
        {

        }

        private void btnsend_Click(object sender, EventArgs e)
        {
            employee EmployeeDetails = new employee();
            EmployeeDetails.employeeName = textEmplyeeName.Text;
            EmployeeDetails.employeeAge = Convert.ToInt32(textemployeeAge.Text);
            EmployeeDetails.employeePosition = textEmployeePosition.Text;
            EmployeeDetails frm = new EmployeeDetails();
            frm.label2.Text = EmployeeDetails.employeeName;
            frm.label3.Text = EmployeeDetails.employeeAge.ToString();
            frm.label4.Text = EmployeeDetails.employeePosition;
            frm.ShowDialog();
        }
    }
}
