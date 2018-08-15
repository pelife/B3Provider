create table `tb_b3_equity_info` 
(
	`id_b3`						integer unique,
	`tx_ticker`					text 	unique,
	`tx_isin`					text 	unique,
	`tx_company_name`			text,
	`tx_description`			text,
	`tx_trading_ccy`			text,
	`vl_market_capitalization`	real,
	`vl_last_price`				real,
	`dt_update`					text,
	primary key(id_b3)
);

create table `tb_b3_option_on_equity_info` 
(
	`id_b3`						integer unique,
	`id_b3_underlying`			integer,
	`tx_ticker`					text unique,
	`tx_isin`					text unique,	
	`tx_description`			text,
	`vl_strike`					real,
	`tx_strike_ccy`				text,
	`tx_style`					text,
	`tx_type`					text,
	`dt_expiration`				text,
	`tx_trading_ccy`			text,	
	`dt_update`					text,
	primary key(id_b3)
);

create table `tb_b3_market_data_info` 
(
	`dt_market_session`							text,
	`id_b3`										integer,
	`tx_ticker`									text,
	`vl_national_financial_volume`				real,
	`tx_national_financial_volume_ccy`			text,
	`vl_international_financial_volume`			real,
	`tx_international_financial_volume_ccy`		text,
	`vl_open_interest`							real,
    `vl_quantity_volume`						int,
    -- best prices (ask bid)
    `vl_best_bid_price`							real,
    `tx_best_bid_price_ccy`						text,
    `vl_best_ask_price`							real,
    `tx_best_ask_price_ccy`						text,    
    -- ohcl (open high close low)
    `vl_first_price`							real,
    `tx_first_price_ccy`						text,
    `vl_minimum_price`							real,
    `tx_minimum_price_ccy`						text,
    `vl_maximum_price`							real,
    `tx_maximum_price_ccy`						text,
    `vl_trade_average_price`					real,
    `tx_trade_average_price_ccy`				text,
    `vl_last_price`								real,
    `tx_last_price_ccy`							text,
    -- volume
    `vl_regular_transaction_quantity`			int,
    `vl_nonregular_transaction_quantity`		int,
    `vl_regular_traded_contracts`				int,
    `vl_nonregular_traded_contracts`			int,
	`vl_national_regular_volume`				real,
    `tx_national_regular_volume_ccy`			text,
	`vl_national_nonregular_volume`				real,
    `tx_national_nonregular_volume_ccy`			text,
    `vl_international_regular_volume`			real,
    `tx_international_regular_volume_ccy`		text,
	`vl_international_nonregular_volume`		real,
    `tx_international_nonregular_volume_ccy`	text,
	-- adjustments
    `vl_adjusted_quote`							real,
    `tx_adjusted_quote_ccy`						text,
    `vl_adjusted_quote_tax`						real,
    `tx_adjusted_quote_tax_ccy`					text,
    `tx_adjusted_quote_tax_situation`			text,    
    `vl_previous_adjusted_quote`				real,
    `tx_previous_adjusted_quote_ccy`			text,
    `vl_previous_adjusted_quote_tax`			real,
    `tx_previous_adjusted_quote_tax_ccy`		text,
    `tx_previous_adjusted_quote_tax_situation`	text,
    -- variation
	`vl_oscillation_percentage`					real,
	`vl_variation_points`						real,
	`tx_variation_points_ccy`					text,
	`vl_equivalent_value`						real,
	`tx_equivalent_value_ccy`					text,
	`vl_adjusted_value_contract`				real,
	`tx_adjusted_value_contract_ccy`			text,
	-- limits
	`vl_maximum_trade_limit`					real,
	`tx_maximum_trade_limit_ccy`				text,
	`vl_minimum_trade_limit`					real,
	`tx_minimum_trade_limit_ccy`				text
	primary key(id_b3,dt_market_data)
);

create table `tb_b3_historic_market_data_info` 
(
	`vl_type`									int,
	`dt_market_session`							text,
	`tx_bdi_code`								text,
	`tx_isin`									text 
	`tx_ticker`									text,
	`tx_market_type_code`						text,
	`tx_short_name`								text,
	`tx_specification`							text,
	`tx_forward_days_to_expiration`				text,
	`tx_trading_ccy`							text,
	 -- ohcl (open high close low)
    `vl_first_price`							real,    
    `vl_minimum_price`							real,    
    `vl_maximum_price`							real,    
    `vl_trade_average_price`					real,    
    `vl_last_price`								real,
    -- best prices (ask bid)
    `vl_best_bid_price`							real,    
    `vl_best_ask_price`							real,
    -- volume
    `vl_regular_transaction_quantity`			int,
    `vl_regular_traded_contracts`				int,
    `vl_national_financial_volume`				real,
    -- options
    `vl_strike`									real,
	`vl_strike_price_change_indicator`			int,
	`dt_expiration`								text,
	`vl_quote_factor`							int,
	`vl_dollar_option_points_strike_price`		real,
	`vl_instrument_distribution_number`			int
);