<script lang="ts">
	import type { Transaction } from '$lib/models/transaction';
	import * as Table from '../ui/table';
	import TransactionItem from './transaction-item.svelte';

	export let transactions: Transaction[];
</script>

<Table.Root>
	<Table.Header>
		<Table.Row>
			<Table.Head>Customer</Table.Head>
			<Table.Head class="xl:table.-column hidden">Type</Table.Head>
			<Table.Head class="xl:table.-column hidden">Status</Table.Head>
			<Table.Head class="xl:table.-column hidden">Date</Table.Head>
			<Table.Head class="text-right">Amount</Table.Head>
		</Table.Row>
	</Table.Header>
	<Table.Body>
		{#each transactions as transaction}
			<TransactionItem {transaction} />
		{/each}
	</Table.Body>
</Table.Root>
