import axios from 'axios';
import React, { useEffect, useState } from 'react';
import DataTable from 'react-data-table-component';
import { utils, writeFile } from 'xlsx';

const ReportCustomer = (props) => {
    const [customerList, setCustomerList] = useState([]);
    useEffect(() => {
        axios.get(process.env.REACT_APP_API_URL + '/Statistic/ReportCustomer')
            .then(res => setCustomerList(res.data))
    }, [])

    const columnsCustomer = [
        {
            name: "Full name",
            selector: (row) => row.fullName,
            sortable: true,
        },
        {
            name: "Total Order Success",
            selector: (row) => row.totalOrderSuccess,
            sortable: true,
        },
        {
            name: 'Total Order Cancelled',
            selector: row => `${row.totalOrderCancelled}`,
            sortable: true,
        },
        {
            name: 'Total Order Waiting',
            selector: row => `${row.totalOrderWaiting}`,
            sortable: true,
        },
        {
            name: 'Total Money Purchased',
            selector: row => `$${row.totalMoneyPurchased}`,
            sortable: true,
        },
    ];
    const HandleOnExport = () => {
        let Heading = [['Full name', 'Total Order Success', 'Total Order Cancelled', 'Total Order Waiting', 'Total Money Purchased']];
        //Had to create a new workbook and then add the header
        const wb = utils.book_new();
        const ws = utils.json_to_sheet([]);
        utils.sheet_add_aoa(ws, Heading);
        //Starting in the second row to avoid overriding and skipping headers
        utils.sheet_add_json(ws, props.data, { origin: 'A2', skipHeader: true });
        utils.book_append_sheet(wb, ws, 'Sheet1');
        writeFile(wb, 'Report.xlsx');

    }


    return (
        <div>
            <DataTable
                title='Top 10 loyal customer'
                columns={columnsCustomer}
                data={customerList}
                fixedHeader
                highlightOnHover
                subHeader
                subHeaderComponent={
                    <button 
                        className="btn btn-primary" 
                        onClick={HandleOnExport}
                        disabled={customerList.length === 0}
                    >
                        <i className="uil uil-print"></i> Export
                    </button>
                }
            />
        </div>
    );
};



export default ReportCustomer;
