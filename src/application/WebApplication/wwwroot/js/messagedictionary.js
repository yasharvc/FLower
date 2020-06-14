const SUBMITTED = "Sent",
	DATA_HAS_ERROR = "Data has error",
	FIELD_IS_EMPTY = "Field is empty",
	FIELD_MUST_NOT_EMPTY = "This field must not be empty",
	SUCCESSFULLY_LOGEDIN = "Successfully logged",
	LOGIN_FAILED = "Failed to authenticate";
const SUCCESSFULLY_CREATED = "Record successfully created",
	FAILURE_IN_CREATING = "There is some error in creating the record";

const codes = {
	1:"Username is already exists"
};

function translateErrorCode(code) {
	return codes[parseInt(code)];
}