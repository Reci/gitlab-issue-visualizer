
function fillUsersDdl() {
    $('#employee').append($('<option>', {
        selected: "selected",
        value: -1,
        text: "Все"
    }));
    $.get("/TimeTable/GetUsers", function (users) {
        $.each(users, function (i, user) {
            if (user.State != "blocked") { //only active users
                if (user.Name != "Administrator" && user.Name != "teamcity") {
                    $('#employee').append($('<option>', {
                        value: user.Id,
                        text: user.Name
                    }));
                };
            };
        });
    })
}


function fillProjectsDdl() {
    $('#project').append($('<option>', {
        selected: "selected",
        value: -1,
        text: "Все"
    }));
    $.get("/TimeTable/GetProjects", function (projects) {
        $.each(projects, function (i, project) {
            $('#project').append($('<option>', {
                value: project.Id,
                text: project.Name
            }));
        });
    });
}


function buildBar(issue) {
    var result = "";
    if (issue.state == "opened") {
        if (Date.parse(issue.created_at) >= Date.parse(firstDayInMonth)) {  //created in current month

            var dayDiff = Math.ceil(Math.abs(Date.parse(issue.created_at) - Date.parse(firstDayInMonth)) / (1000 * 3600 * 24));
            var marginLeft = (dayDiff * 2.8).toPrecision(4);
            var cssClass = "";
            if (Date.parse(issue.due_date) <= Date.parse(lastDayInMonth)) { //deadline in current month
                var daysLength;
                var barLength;
                if (Date.parse(issue.due_date) <= Date.parse(current_date)) {	//overdue
                    cssClass = " bg-danger bordered row";
                    daysLength = Math.ceil(Math.abs(Date.parse(current_date) - Date.parse(issue.created_at)) / (1000 * 3600 * 24));
                    barLength = (daysLength * 2.8).toPrecision(4);
                }
                else {															//in time
                    cssClass = " bg-warning bordered row ";
                    daysLength = Math.ceil(Math.abs(Date.parse(issue.due_date) - Date.parse(issue.created_at)) / (1000 * 3600 * 24));
                    barLength = (daysLength * 2.8).toPrecision(4);
                }
                result = '<div class="' + cssClass + '" style="margin-left:' + marginLeft + '%; width:' + barLength + '%" title="' + issue.title + '">' + issue.title + '</div>';
            }
            else if (Date.parse(issue.due_date) > Date.parse(lastDayInMonth)) { //deadline in next months
                var daysLength = Math.ceil(Math.abs(Date.parse(lastDayInMonth) - Date.parse(issue.created_at)) / (1000 * 3600 * 24));
                var barLength = (daysLength * 2.8).toPrecision(4);
                result = '<div class="row bg-success bordered" style="margin-left:' + marginLeft + '%; width:' + barLength + '%" title="' + issue.title + '">' + issue.title + '</div>';
            }
        }
        else if (Date.parse(issue.due_date) <= Date.parse(firstDayInMonth) || issue.due_date == null)  //deadline in prev months or null
        {
            var dayDiff = Math.ceil(Math.abs(Date.parse(current_date) - Date.parse(firstDayInMonth)) / (1000 * 3600 * 24));
            var barLength = (dayDiff * 2.8).toPrecision(4);
            result = '<div class="row bg-danger row bordered" style="width:' + barLength + '%" title="' + issue.title + '">' + issue.title + '</div>';
        }
    }
    else if (issue.state == "closed") {
        if (Date.parse(issue.updated_at) < Date.parse(firstDayInMonth)) {
            result = "";
        }
        else if (Date.parse(issue.created_at) <= Date.parse(firstDayInMonth) && Date.parse(issue.updated_at) >= Date.parse(firstDayInMonth)) {
            var daysLength = Math.ceil(Math.abs(Date.parse(firstDayInMonth) - Date.parse(issue.updated_at)) / (1000 * 3600 * 24));
            var barLength = (daysLength * 2.8).toPrecision(4);
            result = '<div class="row bg-success bordered" style="width:' + barLength + '%" title="' + issue.title + '">' + issue.title + '</div>';
        }
        else if (Date.parse(issue.created_at) >= Date.parse(firstDayInMonth) && Date.parse(issue.updated_at) >= Date.parse(firstDayInMonth)) {
            var dayDiff = Math.ceil(Math.abs(Date.parse(issue.created_at) - Date.parse(firstDayInMonth)) / (1000 * 3600 * 24));
            var marginLeft = (dayDiff * 2.8).toPrecision(4);
            var daysLength = Math.ceil(Math.abs(Date.parse(issue.updated_at) - Date.parse(issue.created_at)) / (1000 * 3600 * 24));
            var barLength = (daysLength * 2.8).toPrecision(4);
            result = '<div class="row bg-success bordered" style="margin-left:' + marginLeft + '%; width:' + barLength + '%" title="' + issue.title + '">' + issue.title + '</div>';
        }
    }
    return result;
}

function getIssues(project, page = 1) {
    if (page == 0) { return };
    $.get(gitlab_api + "/projects/" + project.id + "/issues?page=" + page + "&private_token=" + private_token, function (data) {
        issues = data;
        $.each(issues, function (index, issue) {
            var userIssuesDiv = "user_" + issue.assignee.id + "_issues";
            var due_date = Date.parse(issue.due_date);
            var cssClass = due_date <= current_date ? "row text-danger" : undefined;
            //$('#'+userIssuesDiv).append(buildDiv("issue",cssClass,"["+project.name+"] "+ issue.title));
            $('#' + userIssuesDiv).append(buildBar(issue));
        })
        if (data.length > 0)
        { page++; }
        else { page = 0 };
        getIssues(project, page);
    });
}