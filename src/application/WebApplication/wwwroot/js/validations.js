function isStringEmpty(val) {
    return new Promise((resolve, reject) => {
        resolve(!!val || FIELD_MUST_NOT_EMPTY);
    });
}