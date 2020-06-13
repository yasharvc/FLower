function postByToken(url, token, data, success, error) {
	data.__RequestVerificationToken = token;
	$.ajax({
		type: "POST",
		url: url,
		data: send,
		success: function (resp) {
			if (success)
				success(resp);
		},
		error: function (a, b, c) {
			if (error)
				error(a, b, c);
		}
	});
}