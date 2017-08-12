
function customizeForTransactionType() {
    var transType = document.getElementById('PURCREFI');

    if (transType.value == 'Refinance') {
        document.getElementById('sellerBlock').style.display = 'none';
        document.getElementById('salesPriceGroup').style.display = 'none';
        document.getElementById('mortgageBrokerBlock').style.display = '';
        document.getElementById('listingBlock').style.display = 'none';
        document.getElementById('sellingBlock').style.display = 'none';
        document.getElementById('buyerLegend').innerHTML = 'Borrower';
        document.getElementById('buyerLegend1').innerHTML = 'Borrower 1';
        document.getElementById('buyerLegend2').innerHTML = 'Borrower 2';
        document.getElementById('buyerOneLabel').innerHTML = 'Name:';
        document.getElementById('buyerTwoLabel').innerHTML = 'Name:';
    }
    else if (transType.value == 'Equity') {
        document.getElementById('sellerBlock').style.display = 'none';
        document.getElementById('salesPriceGroup').style.display = 'none';
        document.getElementById('mortgageBrokerBlock').style.display = 'none';
        document.getElementById('listingBlock').style.display = 'none';
        document.getElementById('sellingBlock').style.display = 'none';
        document.getElementById('buyerLegend').innerHTML = 'Borrower';
        document.getElementById('buyerOneLabel').innerHTML = 'Name:';
        document.getElementById('buyerTwoLabel').innerHTML = 'Name:';
    } else {
        document.getElementById('sellerBlock').style.display = '';
        document.getElementById('salesPriceGroup').style.display = '';
        document.getElementById('mortgageBrokerBlock').style.display = '';
        document.getElementById('listingBlock').style.display = '';
        document.getElementById('sellingBlock').style.display = '';
        document.getElementById('buyerLegend').innerHTML = 'Buyer';
        document.getElementById('buyerOneLabel').innerHTML = 'Name:';
        document.getElementById('buyerTwoLabel').innerHTML = 'Name:';
    }
}

function setContactInformation(role) {
    var contactRole = String(role);
    if (contactRole == 'NONE') {
        document.getElementById('CONTACTNAM').value = '';
        document.getElementById('CONTACTADR1').value = '';
        document.getElementById('CONTACTADR2CITY').value = '';
        document.getElementById('CONTACTADR2STATE').value = '';
        document.getElementById('CONTACTADR2ZIP').value = '';
        document.getElementById('CONTACTPH').value = '';
        document.getElementById('CONTACTEMAIL').value = '';
    } else {
        var contactName;
        if (contactRole.substring(0, 2) == 'AG') {
            contactName = contactRole + 'NAM';
        } else {
            contactName = contactRole + 'CONT';
        }
        document.getElementById('CONTACTNAM').value = document.getElementById(contactName).value;
        document.getElementById('CONTACTADR1').value = document.getElementById(contactRole + 'ADR1').value;
        document.getElementById('CONTACTADR2CITY').value = document.getElementById('ADR2CITY' + contactRole).value;
        document.getElementById('CONTACTADR2STATE').value = document.getElementById('ADR2STATE' + contactRole).value;
        document.getElementById('CONTACTADR2ZIP').value = document.getElementById('ADR2ZIP' + contactRole).value;
        document.getElementById('CONTACTPH').value = document.getElementById(contactRole + 'PH').value;
        document.getElementById('CONTACTEMAIL').value = document.getElementById(contactRole + 'EMAIL').value;
    }
}