(function (m, $, undefined) {

    var globalDeleteLeadUrl = "";

    var globalDeleteOpportunityUrl = "";

    m.Init = function (deleteLeadUrl, deleteOpportunityUrl) {
        globalDeleteLeadUrl = deleteLeadUrl;
        globalDeleteOpportunityUrl = deleteOpportunityUrl;
    }

    $("#confirm-delete").on("hide.bs.modal", function (e) {
        $("#btnLoader").hide();
        $(this).find(".jsBtnDelete").removeAttr("data-confirmedid");
    });

    $(document).on("click", ".jsConfirmDeleteOpportunity", function (e) {
        var opportunityId = $(this).attr("id");
        $("#btnDeleteOpportunity").attr("data-confirmedid", opportunityId);
        $("#btnDeleteOpportunity").show();
        $("#btnCancel").show();
        $("#btnLoader").hide();
        $("#confirm-delete").modal("show");
    });

    $(document).on("click", "#btnDeleteOpportunity", function (e) {
        var $this = $(this);
        $this.hide();
        $("#btnCancel").hide();
        $("#btnLoader").show();

        var confirmedId = $this.attr("data-confirmedid");
        var args = JSON.stringify({ "opportunityId": confirmedId });

        $.ajax({
            cache: false,
            contentType: "application/json;charset=utf-8",
            type: "POST",
            dataType: "JSON",
            url: globalDeleteOpportunityUrl,
            data: args,
            success: function (data) {
                $("#confirm-delete").modal("hide");
                if (data.IsDeleted) {
                    $("#opportunity_" + confirmedId).remove();
                    $("#status-delete").find(".jsSuccessMsg").text(data.Details);
                    $("#status-delete").find(".jsErrorMsg").text('');
                    $("#status-delete").find("#delete-success").show();
                    $("#status-delete").find("#delete-fail").hide();
                } else {
                    $("#status-delete").find(".jsSuccessMsg").text('');
                    $("#status-delete").find(".jsErrorMsg").text(data.Details);
                    $("#status-delete").find("#delete-success").hide();
                    $("#status-delete").find("#delete-fail").show();
                }
                $("#status-delete").modal("show");
                $this.removeAttr("data-confirmedid");
                return false;
            },
            error: function (err) {
                $("#status-delete").find(".jsSuccessMsg").text('');
                $("#status-delete").find(".jsErrorMsg").text(err);
                $("#status-delete").find("#delete-success").hide();
                $("#status-delete").find("#delete-fail").show();
                $("#status-delete").modal("show");
                $this.removeAttr("data-confirmedid");
                return false;
            }
        });
    });

    $(document).on("click", ".jsConfirmDeleteLead", function (e) {
        var leadId = $(this).attr("id");
        $("#btnDeleteLead").attr("data-confirmedid", leadId);
        $("#btnDeleteLead").show();
        $("#btnCancel").show();
        $("#btnLoader").hide();
        $("#confirm-delete").modal("show");
    });

    $(document).on("click", "#btnDeleteLead", function (e) {
        var $this = $(this);
        $this.hide();
        $("#btnCancel").hide();
        $("#btnLoader").show();

        var confirmedId = $this.attr("data-confirmedid");
        var args = JSON.stringify({ "leadid": confirmedId });

        $.ajax({
            cache: false,
            contentType: "application/json;charset=utf-8",
            type: "POST",
            dataType: "JSON",
            url: globalDeleteLeadUrl,
            data: args,
            success: function (data) {
                $("#confirm-delete").modal("hide");
                if (data.IsDeleted) {
                    $("#lead_" + confirmedId).remove();
                    $("#status-delete").find(".jsSuccessMsg").text(data.Details);
                    $("#status-delete").find(".jsErrorMsg").text('');
                    $("#status-delete").find("#delete-success").show();
                    $("#status-delete").find("#delete-fail").hide();
                } else {
                    $("#status-delete").find(".jsSuccessMsg").text('');
                    $("#status-delete").find(".jsErrorMsg").text(data.Details);
                    $("#status-delete").find("#delete-success").hide();
                    $("#status-delete").find("#delete-fail").show();
                }
                $("#status-delete").modal("show");
                $this.removeAttr("data-confirmedid");
                return false;
            },
            error: function (err) {
                alert("failed");
                return false;
            }
        });
    });

}(window.Main = window.Main || {}, jQuery));