let Everlast = {
    Request: {
        DropData: function (location, id, args = null, selectedOption = null) {

            Everlast.Display.ShowLoadOverlay();

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

                    Everlast.Display.HideLoadOverlay();
                },
                fail: function (data) {
                    console.log("Error binding dropdown data to dropdown: " + id);
                    console.log(data.StatusText);
                    Everlast.Display.HideLoadOverlay();
                }
            });
        },
        ObjectData: function (location, args = null) {

            Everlast.Display.ShowLoadOverlay();

            $.ajax({
                type: "POST",
                url: location,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(args),
                success: function (data) {
                    $("#spnPartyTitle").text(data.Title);
                    Everlast.Display.HideLoadOverlay();
                },
                fail: function (data) {
                    console.log("Error requesting object...");
                    console.log(data.StatusText);
                    Everlast.Display.HideLoadOverlay();
                }
            });
        },
        ViewData: function (location, id, args = null) {

            Everlast.Display.ShowLoadOverlay();

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
                    Everlast.Display.HideLoadOverlay();
                },
                fail: function (data) {
                    console.log("Error requesting view...");
                    console.log(data.StatusText);
                    Everlast.Display.HideLoadOverlay();
                }
            });
        },
        DeleteData: function (location, args) {

            Everlast.Display.ShowLoadOverlay();

            $.ajax({
                type: "POST",
                async: false,
                url: location,
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: JSON.stringify(args),
                success: function (data) {
                    if (data > 1) {
                         
                    }

                    Everlast.Display.HideLoadOverlay();
                },
                fail: function (data) {
                    console.log("Error requesting delete...");
                    console.log(data.StatusText);
                    Everlast.Display.HideLoadOverlay();
                }
            });
        },
    },
    Display: {
        ShowLoadOverlay: function () {
            let eLoad = document.getElementById('dvLoading');
            eLoad.style.display = 'block';
        },
        HideLoadOverlay: function () {
            let eLoad = document.getElementById('dvLoading');
            eLoad.style.display = 'none';
        },
        Notification: function (text) {
            let notificationElement = document.createElement("div");
            notificationElement.classList.add('Notification');

            let notificationTextElement = document.createElement("span");
            notificationTextElement.innerText = text;

            notificationElement.appendChild(notificationTextElement);

            let notificationCenterElement = document.getElementById("dvNotificationCenter");

            notificationCenterElement.appendChild(notificationElement);
        }
    },
};

let Helper = {
   
}