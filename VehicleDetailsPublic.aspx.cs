using SoftEra.JHT.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI;

public partial class VehicleDetailsPublic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadVehicleDetails();
        }
    }

    private void LoadVehicleDetails()
    {
        try
        {
            if (Request.QueryString["AppId"] == null ||
                Request.QueryString["Authority"] == null)
            {
                ShowMessage("Invalid Request.");
                return;
            }

            int appId = Convert.ToInt32(Request.QueryString["AppId"]);
            string authority = Request.QueryString["Authority"];

            SqlParameter[] param =
            {
            new SqlParameter("@ACTION","VEHICLE_DETAILS"),
            new SqlParameter("@APP_ID",appId),
            new SqlParameter("@AUTHORITY",authority)
        };

            DataSet ds =
                SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(),
                    CommandType.StoredProcedure,
                    "SP_ROUTE_MONITORING",
                    param);

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ShowMessage("Vehicle details not found.");
                return;
            }

            BindHeader(ds.Tables[0].Rows[0]);

            if (ds.Tables.Count > 1)
                BindJourney(ds.Tables[1]);

            lblApiStatus.Text = "Success";
            lblLastUpdated.Text =
                DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");

            lblSearchDate.Text =
                DateTime.Now.ToString("dd-MMM-yyyy");

            lblSearchTime.Text =
                DateTime.Now.ToString("hh:mm tt");
        }
        catch (Exception ex)
        {
            lblApiStatus.Text = "Error";
            lblLastUpdated.Text = ex.Message;
        }
    }

    private void BindHeader(DataRow dr)
    {
        // ================= SUMMARY =================

        lblRegistration.Text = GetValue(dr, "REGISTRATION_NUMBER");
        lblOwnerName.Text = GetValue(dr, "OWNER_NAME");
        lblVehicleClass.Text = GetValue(dr, "VEHICLE_CLASS_TYPE");
        lblStatus.Text = GetValue(dr, "APPLICATION_STATUS");

        lblPermitNo.Text = GetValue(dr, "PERMIT_NUMBER");
        lblFuel.Text = GetValue(dr, "FUEL_DESC");
        lblRegistrationDate.Text = GetValue(dr, "CREATED_ON");
        lblFitnessValid.Text = GetValue(dr, "FITNESS_VALID_UPTO");

        // ================= OWNER =================

        lblOwner.Text = GetValue(dr, "OWNER_NAME");
        lblFatherName.Text = GetValue(dr, "CARE_OF");
        lblPresentAddress.Text = GetValue(dr, "PRESENT_ADDRESS");
        lblPermanentAddress.Text = GetValue(dr, "PERMANENT_ADDRESS");
        lblRegisteredAt.Text = GetValue(dr, "VEHICLE_REGISTERED_AT");
        lblStatusAsOn.Text = GetValue(dr, "APPLICATION_STATUS");

        // ================= VEHICLE =================

        lblVehicleClassDetail.Text = GetValue(dr, "VEHICLE_CLASS_TYPE");
        lblCategory.Text = GetValue(dr, "PERMIT_TYPE_ID");
        lblFuelType.Text = GetValue(dr, "FUEL_DESC");
        lblColour.Text = GetValue(dr, "VEHICLE_COLOR");
        lblMaker.Text = GetValue(dr, "MAKER_DESC");
        lblMakerModel.Text = GetValue(dr, "MAKER_MODEL");
        lblManufacturingYear.Text = GetValue(dr, "MANUFACTURING_ON");

        // ================= PERMIT =================

        lblPermitNumber.Text = GetValue(dr, "PERMIT_NUMBER");
        lblApplicationStatus.Text = GetValue(dr, "APPLICATION_STATUS");
        lblPermitCategory.Text = GetValue(dr, "PERMIT_TYPE_OPTION_ID");
        lblOffice.Text = GetValue(dr, "VEHICLE_REGISTERED_AT");
        lblPermitVehicleClass.Text = GetValue(dr, "VEHICLE_CLASS_TYPE");

        // ================= VALIDITY =================

        lblFitness.Text = GetValue(dr, "FITNESS_VALID_UPTO");
        lblInsurance.Text = GetValue(dr, "IC_VALID_UPTO");
        lblTax.Text = GetValue(dr, "TAX_PAID_UPTO");
        lblNationalPermit.Text = GetValue(dr, "VALID_UP_TO");
        lblPolicyNo.Text = GetValue(dr, "IDENTITY_PROOF_NO");
        lblInsuranceCompany.Text = GetValue(dr, "PAYMENT_STATUS");

        // ================= TECHNICAL =================

        lblEngineNo.Text = GetValue(dr, "ENGINE_NUMBER");
        lblChassisNo.Text = GetValue(dr, "CHASIS_NUMBER");
        lblULW.Text = GetValue(dr, "ULW");
        lblRLW.Text = GetValue(dr, "RLW");
        lblSeatCapacity.Text = GetValue(dr, "SEATING_CAPACITY");
        lblEmissionNorm.Text = GetValue(dr, "FC_VALID_UPTO");
        lblFinancer.Text = GetValue(dr, "HEAVY_MOTOR_LICENCE");

        // ================= ROUTE =================

        lblRouteVia.Text = GetValue(dr, "ROUTE");
        lblRouteName.Text = GetValue(dr, "ROUTE");

        lblRouteNumber.Text = GetValue(dr, "ROUTE_NUMBER");
        lblRouteDistance.Text = GetValue(dr, "TOTAL_DISTANCE");

        lblAuthority.Text = Request.QueryString["Authority"];

        // ================= SEARCH INFO =================

        lblSearchVehicle.Text = GetValue(dr, "REGISTRATION_NUMBER");
    }

    private void BindJourney(DataTable dt)
    {
        DataView dvUp = new DataView(dt);
        dvUp.RowFilter = "MODE='UP'";

        gvUpJourney.DataSource = dvUp;
        gvUpJourney.DataBind();

        DataView dvDown = new DataView(dt);
        dvDown.RowFilter = "MODE='DOWN'";

        gvDownJourney.DataSource = dvDown;
        gvDownJourney.DataBind();
    }

    private string GetValue(DataRow dr, string column)
    {
        if (!dr.Table.Columns.Contains(column))
            return "--";

        return Convert.ToString(dr[column]);
    }

    private void ShowMessage(string message)
    {
        ScriptManager.RegisterStartupScript(
            this,
            GetType(),
            Guid.NewGuid().ToString(),
            "alert('" + message.Replace("'", "") + "');",
            true);
    }
}