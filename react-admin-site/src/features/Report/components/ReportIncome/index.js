import React from 'react';
import DataTable from 'react-data-table-component';

const ReportIncome = (props) => {
    const handleExport = () => {

    }
    const columns = [
        {
            name: 'Title',
            selector: row => row.title,
        },
        {
            name: 'Year',
            selector: row => row.year,
        },
    ];


    return (
        <div>
            <DataTable
                title='Income'
                columns={columns}
                data={props.data}
                fixedHeader
                selectableRows
                selectableRowsHighlight
                highlightOnHover
                subHeader
                subHeaderComponent={<button className='btn btn-primary' onClick={handleExport}>Export</button>}
            />
        </div>
    );
};


export default ReportIncome;
