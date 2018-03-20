
        $(document).ready(function () {
            //Disable cut copy paste
            $('body').bind('cut copy paste', function (e) {
                e.preventDefault();
            });

            //Disable mouse right click
            $("body").on("contextmenu", function (e) {
                return false;
            });

            $('html').bind('cut copy paste', function (e) {
                e.preventDefault();
            });

            //Disable mouse right click
            $("html").on("contextmenu", function (e) {
                return false;
            });

            document.onkeydown = function (e) {
                if (e.ctrlKey &&
                    (e.keyCode === 67 ||
                     e.keyCode === 86 ||
                     e.keyCode === 85 ||
                     e.keyCode === 117)) {

                    return false;
                } else {
                    return true;
                }
            };

        });





        //    if (document.layers) {
        //        //Capture the MouseDown event.
        //    document.captureEvents(Event.MOUSEDOWN);

        //        //Disable the OnMouseDown event handler.
        //    document.onmousedown = function () {
        //        return false;
        //    };
        //}
        //else {
        ////Disable the OnMouseUp event handler.
        //        document.onmouseup = function (e) {
        //            if (e != null && e.type == "mouseup") {
        //                //Check the Mouse Button which is clicked.
        //                if (e.which == 2 || e.which == 3) {
        //                    //If the Button is middle or right then disable.
        //                    return false;
        //                }
        //            }
        //        };
        //}

        ////Disable the Context Menu event.
        //document.oncontextmenu = function () {
        //    return false;
        //};
  