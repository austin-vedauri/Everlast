let Everlast = {
    Request: {
        DropData: function (location, id, args = null) {

            let option, drop;
            drop = document.getElementById(id);
            debugger
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

                    } else {
                        option = document.createElement("option");
                        option.text = "No Options...";
                        option.value = "-1";
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
        }
    },
};

let Helper = {
   
}