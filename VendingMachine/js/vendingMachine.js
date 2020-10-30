$(document).ready(function () {

  loadVendingMachine();
  var changeInput = $('#totalInput').val();
  var candyCost;
  var quantityLeft;
  const formatter = new Intl.NumberFormat('en-US', {
   minimumFractionDigits: 2,
   maximumFractionDigits: 2,
});
  function loadVendingMachine() {

  $.ajax({
    type: 'GET',
    url: 'http://localhost:8080/items',
    success: function(itemArray) {
      $.each(itemArray, function(index, item) {
        var itemId = item.id;
        var itemName = item.name;
        var itemPrice = item.price;
        var itemQuantity = item.quantity;

        $('#candy-name-' + itemId).append(itemName);
        $('#candy-price-' + itemId).append('$' +itemPrice);
        $('#quantity-left-' + itemId).append('Quantity Left: ' + itemQuantity);

      });
    },
    error: function() {
      $('#errorMessages')
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text('Error calling web service. Please try again later.'));
    }
  });
  }
  $("#add-dollar-button").on("click", function() {

      if(changeInput == '')
      {
        changeInput = 0;
      }

        parseFloat(changeInput);

      changeInput = changeInput + 1.00;
      $('#totalInput').val(formatter.format(changeInput));
})
$("#add-dime-button").on("click", function() {

      if(changeInput == '')
      {
        changeInput = 0;
      }

      parseFloat(changeInput);
      changeInput = changeInput + .10;
      $('#totalInput').val(formatter.format(changeInput));
})
$("#add-quarter-button").on("click", function() {

      if(changeInput == '')
      {
        changeInput = 0;
      }

      parseFloat(changeInput);
      changeInput = changeInput + .25;
      $('#totalInput').val(formatter.format(changeInput));
})
$("#add-nickel-button").on("click", function() {

      if(changeInput == '')
      {
        changeInput = 0;
      }

      parseFloat(changeInput);
      changeInput = changeInput + .05;
      $('#totalInput').val(formatter.format(changeInput));
})
$('.card').on("click", function() {
  var candyId = $(this).find('.card-header').text();
  var cost = $('#candy-price-' + candyId).text();
  candyCost = parseFloat(cost.slice(1));
  quantityLeft = $('#quantity-left-' + candyId).text();
  $('#itemInput').val(candyId);
})
$('#make-purchase').on("click", function() {

    $.ajax({
      type: 'GET',
      url: 'http://localhost:8080/money/' + $('#totalInput').val() + '/item/' + $('#itemInput').val(),
      success: function(data) {
        var quarters = data.quarters;
        var dimes = data.dimes;
        var nickels = data.nickels;
        var pennies = data.pennies;

        var change = '';

        if(quarters > 0)
        {
          change = change + quarters + ' ' + 'Quarter' + ' ';
        }
        if(dimes > 0)
        {
          change = change + dimes + ' ' + 'Dime' + ' ';
        }
        if(nickels > 0)
        {
          change = change + nickels + ' ' + 'Nickel' + ' ';
        }
        if(pennies > 0 )
        {
          change = change + pennies + ' ' + 'Penny' + ' ';
        }

        $('#changeInput').val(change);

        $('#messagesInput').val("Thank You!!!")
      },
      error: function(data) {
        var message = data.responseJSON.message;
        clearMessages();
        $('#messagesInput').val(message);
      }
    })
})

$('#change-return').on("click", function() {
  resetVendingMachine();
})

function clearMessages() {
  $('#messagesInput').val('');
}


function clearVendingMachine() {
  $('.card-text').empty();
  $('.card-title').empty();
  $('.card-footer').empty();
}

function resetVendingMachine() {
    clearVendingMachine();
    loadVendingMachine();
    changeInput = 0;
    $('#totalInput').val('');
    $('#changeInput').val('');
    $('#messagesInput').val('');
    $('#itemInput').val('');
}
})
