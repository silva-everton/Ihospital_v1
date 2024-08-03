using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ihospital_v1
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            // Get the values from the form
            string givenName = txtGivenName.Text;
            string lastName = txtLastName.Text;
            DateTime dob = DateTime.Parse(txtDOB.Text); 
            string contactPhone = txtContactPhone.Text;

            // Placeholder for saving the data or any other processing
            
            lblMessage.Text = "Registration successful!";

        }
    }
}