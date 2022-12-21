import React from 'react';
import DataTable from 'react-data-table-component';
import { utils, writeFile } from 'xlsx';

const ReportCustomer = (props) => {

    const HandleOnExport = () => {
        let Heading = [['Category', 'Total', 'Assigned']];
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
                columns={props.columns}
                data={props.data}
                fixedHeader
                highlightOnHover
                subHeader
                subHeaderComponent={<button className="btn btn-primary" onClick={HandleOnExport}><i className="uil uil-print"></i> Export</button>}
            />
        </div>
    );
};



export default ReportCustomer;
