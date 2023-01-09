import CustomLoader from 'components/CustomLoader/Index';
import { fetchProducts } from 'features/Product/ProductSlice';
import React, { useEffect, useState } from 'react';
import DataTable from 'react-data-table-component';
import { useDispatch, useSelector } from 'react-redux';
import { utils, writeFile } from 'xlsx';

const ReportProduct = (props) => {
    const dispatch = useDispatch();
    let loading = useSelector((state) => state.products.loading);
    let products = useSelector((state) => state.products.products);
    const [productList, setProductList] = useState([]);
    useEffect(() => {
        var list = products.slice().sort((a, b) => b.income - a.income).filter((element, index) => index < 10);
        var convertList = list.map(item => {
            return {
                productId: item.productId,
                productName: item.productName,
                income: item.income,
            }
        });
        setProductList(convertList);
    }, [products])

    useEffect(() => {
        dispatch(fetchProducts());
    }, [])
    const columnsProduct = [
        {
            name: "Product Id",
            selector: (row) => row.productId,
            sortable: true,
        },
        {
            name: "Name",
            selector: (row) => row.productName,
            sortable: true,
        },
        {
            name: 'Income',
            selector: row => `$${row.income}`,
            sortable: true,
        },
    ];
    const HandleOnExport = () => {
        let Heading = [['ID', 'Name', 'Total money']];
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
                title='Top 10 product'
                columns={columnsProduct}
                data={productList}
                fixedHeader
                highlightOnHover
                subHeader
                subHeaderComponent={
                    <button 
                        className="btn btn-primary" 
                        onClick={HandleOnExport}
                        disabled={productList.length === 0}
                    >
                        <i className="uil uil-print"></i> Export
                    </button>
                }
                progressPending={loading}
                progressComponent={
                    <div>
                        {Array(10)
                            .fill("")
                            .map((e, i) => (
                                <CustomLoader key={i} style={{ opacity: Number(2 / i).toFixed(1) }} />
                            ))}
                    </div>
                }
            />
        </div>
    );
};


export default ReportProduct;
