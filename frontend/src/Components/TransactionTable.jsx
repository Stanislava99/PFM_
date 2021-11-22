import React, { Component } from 'react';
import TransactionService from '../Services/TransactionService';
import BootstrapTable from 'react-bootstrap-table-next'; 
import filterFactory, { textFilter } from 'react-bootstrap-table2-filter';
import paginationFactory from 'react-bootstrap-table2-paginator';

export class TransactionTable extends Component {

    constructor(props) {
        super(props);

        this.state = {
            transactions:[],
            columns:[
                {
                    dataField:'id',
                    text:"ID"
                },
                {
                    dataField:'beneficiary-name',
                    text:"Beneficiary Name"
                },
                {
                    dataField:'date',
                    text:"Date"
                },
                {
                    dataField:'direction',
                    text:"Direction",
                    sort: true
                },
                {
                    dataField:'amount',
                    text:"Amount"
                },
                {
                    dataField:'description',
                    text:"Description"
                },
                {
                    dataField:'currency',
                    text:"Currency"
                },
                {
                    dataField:'mcc',
                    text:"MCC",
                    filter: textFilter()
                },
                {
                    dataField:'kind',
                    text:"Kind"
                }
            ]
        };

    }


    componentDidMount() {
        TransactionService.getTransactions().then (response =>{
            this.setState({transactions: response.data});    
        })
    }

    render() {
        const options = {
            page:1,
            sizePerPageList:[
                {
                    text:'5', value:5
                },
                {
                    text:'10', value:10
                },
                {
                    text:'15',value:15
                }
            ],
            sizePerPage: 10,
            pageStartIndex:1,
            paginationSize:5,
            prevPage: 'Prev',
            nextPage: 'Next',
            firstPage: 'First',
            lastPage: 'Last'
        };
        return (
            <div className = "containet">
                <div class = "row" className="hdr" style={{marginTop:"20px", marginRight:"20px"}}>
                    <div class = "col-sm-12 btn btn-info" style={{backgroundColor:"darkSlateGrey", color:"white", border:"none"}}> Transactions Table </div>
                </div>
                <div style={{marginTop:"20px", marginRight:"20px"}}>
                    <BootstrapTable
                    striped
                    hover
                    keyField='id'
                    data={this.state.transactions}
                    columns={this.state.columns}
                    filter={filterFactory()}
                    pagination={paginationFactory(options)}
                    />
                </div>
            </div>
        )
    }
}

export default TransactionTable