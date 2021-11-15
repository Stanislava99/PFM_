import React, { Component } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Button } from 'primereact/button';
import { Ripple } from 'primereact/ripple';
import { Dropdown } from 'primereact/dropdown';
import { InputText } from 'primereact/inputtext';
import CustomerService from '../Services/CostumerService';
import { classNames } from 'primereact/utils';

export class TransactionTable extends Component {

    constructor(props) {
        super(props);

        this.state = {
            customers1: [],
            customers2: [],
            customers3: [],
            first1: 0,
            rows1: 10,
            first2: 0,
            rows2: 10,
            currentPage: 1,
            pageInputTooltip: 'Press \'Enter\' key to go to this page.'
        };

        this.onCustomPage1 = this.onCustomPage1.bind(this);
        this.onCustomPage2 = this.onCustomPage2.bind(this);
        this.onPageInputKeyDown = this.onPageInputKeyDown.bind(this);
        this.onPageInputChange = this.onPageInputChange.bind(this);
    }

    onCustomPage1(event) {
        this.setState({
            first1: event.first,
            rows1: event.rows,
            currentPage: event.page + 1
        });
    }

    onCustomPage2(event) {
        this.setState({
            first2: event.first,
            rows2: event.rows
        });
    }

    onPageInputKeyDown(event, options) {
        if (event.key === 'Enter') {
            const page = parseInt(this.state.currentPage);
            if (page < 0 || page > options.totalPages) {
                this.setState({ pageInputTooltip: `Value must be between 1 and ${options.totalPages}.`})
            }
            else {
                const first = this.state.currentPage ? options.rows * (page - 1) : 0;

                this.setState({ first1: first, pageInputTooltip: 'Press \'Enter\' key to go to this page.' });
            }
        }
    }

    onPageInputChange(event) {
        this.setState({ currentPage: event.target.value });
    }

    componentDidMount() {
        CustomerService.getCustomersLarge().then(data => this.setState({ customers1: data }));
        CustomerService.getCustomersLarge().then(data => this.setState({ customers2: data }));
        CustomerService.getCustomersLarge().then(data => this.setState({ customers3: data }));
    }

    render() {
        const paginatorLeft = <Button type="button" icon="pi pi-refresh" className="p-button-text" />;
        const paginatorRight = <Button type="button" icon="pi pi-cloud" className="p-button-text" />;
        const template1 = {
            layout: 'PrevPageLink PageLinks NextPageLink RowsPerPageDropdown CurrentPageReport',
            'PrevPageLink': (options) => {
                return (
                    <button type="button" className={options.className} onClick={options.onClick} disabled={options.disabled}>
                        <span className="p-p-3">Previous</span>
                        <Ripple />
                    </button>
                )
            },
            'NextPageLink': (options) => {
                return (
                    <button type="button" className={options.className} onClick={options.onClick} disabled={options.disabled}>
                        <span className="p-p-3">Next</span>
                        <Ripple />
                    </button>
                )
            },
            'PageLinks': (options) => {
                if ((options.view.startPage === options.page && options.view.startPage !== 0) || (options.view.endPage === options.page && options.page + 1 !== options.totalPages)) {
                    const className = classNames(options.className, { 'p-disabled': true });

                    return <span className={className} style={{ userSelect: 'none' }}>...</span>;
                }

                return (
                    <button type="button" className={options.className} onClick={options.onClick}>
                        {options.page + 1}
                        <Ripple />
                    </button>
                )
            },
            'RowsPerPageDropdown': (options) => {
                const dropdownOptions = [
                    { label: 10, value: 10 },
                    { label: 20, value: 20 },
                    { label: 50, value: 50 },
                    { label: 'All', value: options.totalRecords }
                ];

                return <Dropdown value={options.value} options={dropdownOptions} onChange={options.onChange} appendTo={document.body} />;
            },
            'CurrentPageReport': (options) => {
                return (
                    <span className="p-mx-3" style={{ color: 'var(--text-color)', userSelect: 'none' }}>
                        Go to <InputText size="2" className="p-ml-1" value={this.state.currentPage} tooltip={this.state.pageInputTooltip}
                            onKeyDown={(e) => this.onPageInputKeyDown(e, options)} onChange={this.onPageInputChange}/>
                    </span>
                )
            }
        };
        const template2 = {
            layout: 'RowsPerPageDropdown CurrentPageReport PrevPageLink NextPageLink',
            'RowsPerPageDropdown': (options) => {
                const dropdownOptions = [
                    { label: 10, value: 10 },
                    { label: 20, value: 20 },
                    { label: 50, value: 50 }
                ];

                return (
                    <React.Fragment>
                        <span className="p-mx-1" style={{ color: 'var(--text-color)', userSelect: 'none' }}>Items per page: </span>
                        <Dropdown value={options.value} options={dropdownOptions} onChange={options.onChange} appendTo={document.body} />
                    </React.Fragment>
                );
            },
            'CurrentPageReport': (options) => {
                return (
                    <span style={{ color: 'var(--text-color)', userSelect: 'none', width: '120px', textAlign: 'center' }}>
                        {options.first} - {options.last} of {options.totalRecords}
                    </span>
                )
            }
        };

        return (
            <div>
                <div className="card">
                    <h5>Basic</h5>
                    <DataTable value={this.state.customers1} paginator responsiveLayout="scroll"
                        paginatorTemplate="CurrentPageReport FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink RowsPerPageDropdown"
                        currentPageReportTemplate="Showing {first} to {last} of {totalRecords}" rows={10} rowsPerPageOptions={[10,20,50]}
                        paginatorLeft={paginatorLeft} paginatorRight={paginatorRight}>
                        <Column field="name" header="Name"></Column>
                        <Column field="country.name" header="Country"></Column>
                        <Column field="company" header="Company"></Column>
                        <Column field="representative.name" header="Representative"></Column>
                    </DataTable>
                </div>

                <div className="card">
                    <h5>Custom Paginator Template</h5>
                    <DataTable value={this.state.customers2} paginator paginatorTemplate={template1} first={this.state.first1} rows={this.state.rows1} onPage={this.onCustomPage1} responsiveLayout="scroll">
                        <Column field="name" header="Name"></Column>
                        <Column field="country.name" header="Country"></Column>
                        <Column field="company" header="Company"></Column>
                        <Column field="representative.name" header="Representative"></Column>
                    </DataTable>

                    <DataTable value={this.state.customers3} paginator paginatorTemplate={template2} first={this.state.first2} rows={this.state.rows2} onPage={this.onCustomPage2}
                        paginatorClassName="p-jc-end" className="p-mt-6" responsiveLayout="scroll">
                        <Column field="name" header="Name"></Column>
                        <Column field="country.name" header="Country"></Column>
                        <Column field="company" header="Company"></Column>
                        <Column field="representative.name" header="Representative"></Column>
                    </DataTable>
                </div>
            </div>
        );
    }
}