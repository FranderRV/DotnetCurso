function AJAX(route, NewData, typeRequest = "GET", callback) {
  $.ajax({
    type: typeRequest,
    url: route,
    data: NewData,
    success: callback,
    error: callback,
  });
}

