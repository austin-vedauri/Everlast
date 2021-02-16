let Everlast = {
    Request: {
        DropData: function (location, id, args = null, selectedOption = null) {

            let option, drop;
            drop = document.getElementById(id);
            $.ajax({
                type: "POST",
                url: location,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(args),
                success: function (data) {

                    if (data.length > 0) {

                        $.each(data, function (i, data) {
                            option = document.createElement("option");
                            option.text = data.Text;
                            option.value = data.Value;
                            drop.appendChild(option);
                        });

                        if (selectedOption != null) {
                            drop.value = selectedOption;
                        } else {
                            // 
                        }

                    } else {

                        option = document.createElement("option");
                        option.text = "No Options...";
                        option.value = "0";
                        drop.appendChild(option);

                    }
                },
                fail: function (data) {
                    console.log("Error binding dropdown data to dropdown: " + id);
                    console.log(data.StatusText);
                }
            });
        },
        ObjectData: function (location, args = null) {
            $.ajax({
                type: "POST",
                url: location,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(args),
                success: function (data) {
                    $("#spnPartyTitle").text(data.Title);
                },
                fail: function (data) {
                    console.log("Error requesting object...");
                    console.log(data.StatusText);
                }
            });
        },
        ViewData: function (location, id, args = null) {
            $.ajax({
                type: "POST",
                async: false,
                url: location,
                contentType: "application/json; charset=utf-8",
                dataType: "HTML",
                data: JSON.stringify(args),
                success: function (data) {
                    $("#" + id).html("");
                    $("#" + id).html(data);
                },
                fail: function (data) {
                    console.log("Error requesting view...");
                    console.log(data.StatusText);
                }
            });
        },
        DeleteData: function (location, args) {
            $.ajax({
                type: "POST",
                async: false,
                url: location,
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: JSON.stringify(args),
                success: function (data) {
                    debugger
                    alert(data);
                },
                fail: function (data) {
                    console.log("Error requesting delete...");
                    console.log(data.StatusText);
                }
            });
        },
    },
};

let Helper = {
   
}