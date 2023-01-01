export const CompactText =(text, maxlength = 15) => {
    var result = "";

    if (text != null) {
        if (text.length <= maxlength)
            return text;

        else {
            for (var i = 0; i < maxlength; i++) {
                result = result.concat(text.split('')[i])
            }
            return result + "...";
        }
    }
    else
        return result;
}


export const FormatDateTime = (datetime) => {
    if (datetime != null) {
    let date = `${datetime.split("T")[0].split("-")[2]}-${datetime.split("T")[0].split("-")[1]}-${datetime.split("T")[0].split("-")[0]}`;
    let time = `${datetime.split("T")[1].split(":")[0]}:${datetime.split("T")[1].split(":")[1]}`;
    return `${date} ${time}`;
    }
    return "";
} 

export const formatDate = (date) => {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) 
        month = '0' + month;
    if (day.length < 2) 
        day = '0' + day;

    return [year, month, day].join('-');
}

export const UTCWithoutHour = (date) => {
    return new Date(
        Date.UTC(
            date.getFullYear(),
            date.getMonth(),
            date.getDate(),
        )
    );
}


export const GetDate = (dateString) => {
    var date = new Date(dateString);
    var day = date.getDate();
    var month = date.getMonth() + 1; //January is 0!
    var year = date.getFullYear();

    if (day < 10) {
        day = `0${day}`
    }

    if (month < 10) {
        month = `0${month}`
    }

    return `${year}-${month}-${day}`;
}

export const FormatStatusOrder = (orderStatus) => {
    let result;
    if (orderStatus === 1)
        result = `New Order`
    else if (orderStatus === 2)
        result = `Waiting Delivery`
    else if (orderStatus === 3)
        result = `Delivered`
    else if (orderStatus === 4)
        result = `Cancelled`

    return result;
}