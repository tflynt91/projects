$(document).ready(function () {

  loadDvds();

  $('#createDvdButton').on('click', function(){
    $('#createDvdSection').show();
    $('#searchTableDiv').hide();
  });

  $('#searchButton').click(function (event){

    var searchCriteria = $('#searchDropDown :selected').text();

    var haveValidationErrors = checkAndDisplaySearchDvdValidationErrors(searchCriteria, ($('#searchField').val()));

    if(haveValidationErrors) {
      return false;
    }


    clearDvdTable();




    switch(searchCriteria) {
      case 'Title':
        $.ajax({
          type: 'GET',
          url: 'https://localhost:44383/dvds/title/' + $('#searchField').val(),
          success: function(result) {
            makeDvdArray(result);
          },
          error: function () {
            $('#errorMessages')
              .append($('<li>')
              .attr({class: 'list-group-item list-group-item-danger'})
              .text('Error calling web service. Please try again later.'));
        }
      });
      break;
      case 'Release Year':
        $.ajax({
          type: 'GET',
          url: 'https://localhost:44383/dvds/year/' + $('#searchField').val(),
          success: function(result) {
            makeDvdArray(result);
          },
          error: function () {
            $('#errorMessages')
              .append($('<li>')
              .attr({class: 'list-group-item list-group-item-danger'})
              .text('Error calling web service. Please try again later.'));
        }
      });
      break;
      case 'Director Name':
      $.ajax({
        type: 'GET',
        url: 'https://localhost:44383/dvds/director/' + $('#searchField').val(),
        success: function(result) {
          makeDvdArray(result);
        },
        error: function() {
          $('#errorMessages')
              .append($('<li>')
              .attr({class: 'list-group-item list-group-item-danger'})
              .text('Error calling web service. Please try again later.'));
        }
      });
      break;
      case 'Rating':
      $.ajax({
        type: 'GET',
        url: 'https://localhost:44383/dvds/rating/' + $('#searchField').val(),
        success: function(result) {
          makeDvdArray(result);
        },
        error: function() {
          $('#errorMessages')
              .append($('<li>')
              .attr({class: 'list-group-item list-group-item-danger'})
              .text('Error calling web service. Please try again later.'));
        }
      });
      break;
      default: makeDvdArray();

    }
  })

  $('#create-dvd-button').click(function (event){

    var haveValidationErrors = checkAndDisplayEditAndCreateDvdValidationErrors(($('#add-release-year').val()), ($('#add-title').val()));

    if(haveValidationErrors) {
      return false;
    }

    $.ajax({
      type: 'POST',
      url: 'https://localhost:44383/dvd',
      data: JSON.stringify({
        title: $('#add-title').val(),
        releaseYear: $('#add-release-year').val(),
        director: $('#add-director').val(),
        ratingType: $('#choose-rating :selected').text(),
        notes: $('#add-notes').val()
      }),
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      'dataType': 'json',
      success: function(data, status) {
        $('#errorMessages').empty();
        $('#createDvdSection').toggle();
        $('#searchTableDiv').toggle();
        loadDvds();

      },
      error: function() {
        $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service. Please try again later.'));
      }
    })
  });

  $('#edit-dvd-button').click(function(event) {

    var haveValidationErrors = checkAndDisplayEditAndCreateDvdValidationErrors(($('#edit-release-year').val()), ($('#edit-title').val()));

    if(haveValidationErrors) {
      return false;
    }

    $.ajax({
      type: 'PUT',
      url: 'https://localhost:44383/dvd/' + $('#edit-dvd-id').val(),
      data: JSON.stringify({
        dvdId: $('#edit-dvd-id').val(),
        title: $('#edit-title').val(),
        releaseYear: $('#edit-release-year').val(),
        director: $('#edit-director').val(),
        ratingType: $('#edit-rating :selected').text(),
        notes: $('#edit-notes').val()
      }),
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      'dataType': 'json',
      success: function() {
        $('#errorMessages').empty();
        hideEditForm();
        loadDvds();
      },
      error: function() {
        $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service. Please try again later.'));
      }

    })
  })

});

function loadDvds() {
  clearDvdTable();
  var contentRows = $('#contentRows');

  $.ajax({
    type: 'GET',
    url: 'https://localhost:44383/dvds',
    success: function(dvdArray) {
      $.each(dvdArray, function(index, dvd) {
        var title = dvd.title;
        var releaseYear = dvd.releaseYear;
        var director = dvd.director;
        var rating = dvd.ratingType;
        var dvdId = dvd.dvdId;

        var row = '<tr>';
            row += '<td>' + title + '</td>';
            row += '<td>' + releaseYear + '</td>';
            row += '<td>' + director + '</td>';
            row += '<td>' + rating + '</td>';
            row += '<td><a onclick="showEditForm(' + dvdId + ')" class="btn btn-light btn-sm mx-1 active" role="button">Edit</a>'
            + '<a onclick="deleteDvd(' + dvdId + ')" class="btn btn-light btn-sm mx1 active" role="button">Delete</a></td>';
            row += '</tr>';

        contentRows.append(row);
      });
    },
    error: function() {
      $('#errorMessages')
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text('Cant Load Table. Please try again later.'));
    }
  });
}

function clearDvdTable() {
  $('#contentRows').empty();
}

function showEditForm(dvdId) {
  $('#errorMessages').empty();

  $.ajax({
    type: 'GET',
    url: 'https://localhost:44383/dvd/' + dvdId,
    success: function(data, status) {
      $('#edit-title').val(data.title);
      $('#edit-release-year').val(data.releaseYear);
      $('#edit-director').val(data.director);
      $('#edit-rating').val(data.ratingType);
      $('#edit-notes').val(data.notes);
      $('#edit-dvd-id').val(data.dvdId);
    },
    error: function() {
      $('#errorMessages')
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text('Error calling web service. Please try again later.'));
    }
  })
  $('#searchTableDiv').hide();
  $('#editDvdSection').show();
}

function hideEditForm() {
  $('#errorMessages').empty();

  $('#edit-title').val('');
  $('#edit-release-year').val('');
  $('#edit-director').val('');
  $('#edit-rating').prop('selected', function () {
    return this.defaultSelected;
  })
  $('#edit-notes').val('');

  $('#editDvdSection').hide();
  $('#searchTableDiv').show();
}

function deleteDvd(dvdId) {
  $.ajax({
    type: 'DELETE',
    url: 'https://localhost:44383/dvd/' + dvdId,
    success: function() {
      loadDvds();
    }
  });
}

function checkAndDisplayEditAndCreateDvdValidationErrors(releaseYear, title) {

  $('#errorMessages').empty();

  var errorMessages = [];


    if(releaseYear.length !== 4) {
      var errorField = 'Please enter a 4-digit year';
      errorMessages.push(errorField);
    }
    if(!title.length > 0) {
      var errorField = 'Please enter a title for the Dvd.';
      errorMessages.push(errorField);
    }

  if (errorMessages.length > 0){
    $.each(errorMessages,function(index,message){
      $('#errorMessages').append($('<li>').attr({class: 'list-group-item list-group-item-danger'}).text(message));
    });

    return true;
  } else {
    return false;
  }
}

function checkAndDisplaySearchDvdValidationErrors(menuOption, searchField) {

  $('#errorMessages').empty();

  var errorMessages = [];

  var validSearchOptions = ["Title", "Release Year", "Director Name", "Rating"];



    if(!validSearchOptions.includes(menuOption)) {
      var errorField = 'Search Category is required';
      errorMessages.push(errorField);
    }
    if(!searchField.length > 0) {
      var errorField = 'Search Term is required';
      errorMessages.push(errorField);
    }

  if (errorMessages.length > 0){
    $.each(errorMessages,function(index,message){
      $('#errorMessages').append($('<li>').attr({class: 'list-group-item list-group-item-danger'}).text(message));
    });

    return true;
  } else {
    return false;
  }
}

function makeDvdArray(dvdArray) {

  var contentRows = $('#contentRows');

  $.each(dvdArray, function(index, dvd) {
    var title = dvd.title;
    var releaseYear = dvd.releaseYear;
    var director = dvd.director;
    var rating = dvd.ratingType;
    var dvdId = dvd.dvdId;

    var row = '<tr>';
        row += '<td>' + title + '</td>';
        row += '<td>' + releaseYear + '</td>';
        row += '<td>' + director + '</td>';
        row += '<td>' + rating + '</td>';
        row += '<td><a onclick="showEditForm(' + dvdId + ')" class="btn btn-light btn-sm mx-1 active" role="button">Edit</a>'
        + '<a onclick="deleteDvd(' + dvdId + ')" class="btn btn-light btn-sm mx1 active" role="button">Delete</a></td>';
        row += '</tr>';

    contentRows.append(row);
  })
}
