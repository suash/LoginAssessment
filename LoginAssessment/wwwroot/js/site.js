function register(postData, handleLogin) {
    $.ajax({
        url: "./register",
        contentType: "application/json",
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: "JSON",
        success: function (res) {
            let response = {
                success: true,
                response: res
            };

            return handleLogin(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            let response = {
                success: false,
                code: xhr.status,
                response: xhr.statusText
            };

            return handleLogin(response);
        }
    });
}

function login_password(postData, handleLogin) {
    $.ajax({
        url: "./loginpass",
        contentType: "application/json",
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: "JSON",
        success: function (res) {
            let response = {
                success: true,
                response: res
            };

            return handleLogin(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            let response = {
                success: false,
                code: xhr.status,
                response: xhr.statusText
            };

            return handleLogin(response);
        }
    });
}

function login_passphrase(postData, handleLogin) {
    $.ajax({
        url: "./loginphrase",
        contentType: "application/json",
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: "JSON",
        success: function (res) {
            let response = {
                success: true,
                response: res
            };

            return handleLogin(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            let response = {
                success: false,
                code: xhr.status,
                response: xhr.statusText
            };

            return handleLogin(response);
        }
    });
}

function update_password(postData, handleLogin) {
    $.ajax({
        url: "./updatepass",
        contentType: "application/json",
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: "JSON",
        success: function (res) {
            let response = {
                success: true,
                response: res
            };

            return handleLogin(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            let response = {
                success: false,
                code: xhr.status,
                response: xhr.statusText
            };

            return handleLogin(response);
        }
    });
}

function update_passphrase(postData, handleLogin) {
    $.ajax({
        url: "./updatephrase",
        contentType: "application/json",
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: "JSON",
        success: function (res) {
            let response = {
                success: true,
                response: res
            };

            return handleLogin(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            let response = {
                success: false,
                code: xhr.status,
                response: xhr.statusText
            };

            return handleLogin(response);
        }
    });
}


function failed_login(postData, handleLogin) {
    $.ajax({
        url: "./failedlogin",
        contentType: "application/json",
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: "JSON",
        success: function (res) {
            let response = {
                success: true,
                response: res
            };

            return handleLogin(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            let response = {
                success: false,
                code: xhr.status,
                response: xhr.statusText
            };

            return handleLogin(response);
        }
    });
}

function success_login(postData, handleLogin) {
    $.ajax({
        url: "./successlogin",
        contentType: "application/json",
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: "JSON",
        success: function (res) {
            let response = {
                success: true,
                response: res
            };

            return handleLogin(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            let response = {
                success: false,
                code: xhr.status,
                response: xhr.statusText
            };

            return handleLogin(response);
        }
    });
}