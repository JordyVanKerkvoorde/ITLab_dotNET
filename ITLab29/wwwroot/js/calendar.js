// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var events = [];
    $.ajax({
        type: "GET",
        url: "/session/get",
        success: function (data) {
            $.each(data, function (i, v) {
                events.push({
                    sessionId: v.sessionId,
                    title: '\n' + v.title,
                    description: v.description,
                    start: v.start,
                    end: v.end
                });
            })

            GenerateCalender(events);
        },
        error: function (error) {
            alert('failed');
        }
    })

    function GenerateCalender(events) {
        $('#calender').fullCalendar('destroy');
        $('#calender').fullCalendar({
            contentHeight: 600,
            navLinks: true, // can click day/week names to navigate views
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,listMonth'
            },
            eventLimit: true,
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                window.location.href = "/session/" + calEvent.sessionId;
            },
            eventRender: (eventObj, $el) => {
                $el.popover({
                    title: eventObj.title,
                    content: eventObj.description,
                    trigger: 'hover',
                    placement: 'top',
                    container: 'body'
                });
            }
        })
    }
})
